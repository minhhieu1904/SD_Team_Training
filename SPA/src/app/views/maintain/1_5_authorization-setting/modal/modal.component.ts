import { Component, ElementRef, Input, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { Users } from '@models/common/users';
import { RolesUserStatus } from '@models/maintain/authorization-setting';
import { ModalService } from '@services/common/modal.service';
import { AuthorizationSettingService } from '@services/maintain/authorization-setting.service';
import { InjectBase } from '@utilities/inject-base-app';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss']
})
export class ModalComponent extends InjectBase implements OnInit {
  @Input() id: string;
  @ViewChild('modalAuthorizationUser', { static: false }) modalAuthorizationUser?: ModalDirective;
  iconButton = IconButton;
  userAuthorization: Users = <Users>{};
  rolesUser: RolesUserStatus[] = [];
  rolesUserConst: RolesUserStatus[] = [];

  constructor(
    private service: AuthorizationSettingService,
    private modalService: ModalService,
    private el: ElementRef
  ) {
    super();
  }

  ngOnInit() {
    if (!this.id)
      return;
    this.modalService.add(this);
    this.service.layoutSource.asObservable().subscribe(res => {
      if (res) {
        this.userAuthorization = res;
        this.getRoleUser(res.account);
      }
    });
  }

  open(): void {
    this.modalAuthorizationUser.show();
  }

  getRoleUser(account: string) {
    this.spinnerService.show();
    this.service.getRoleUser(account).subscribe({
      next: (res) => {
        this.rolesUserConst = res;
        this.rolesUser = JSON.parse(JSON.stringify(this.rolesUserConst));
      },
      error: () => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
      },
      complete: () => {
        this.spinnerService.hide();
      }
    });
  }

  updateRolesUser() {
    let checkChangeRole = JSON.stringify(this.rolesUserConst) === JSON.stringify(this.rolesUser);
    if (checkChangeRole) {
      this.snotifyService.error('Nothing has changed, Please change role of user!!!', CaptionConstants.ERROR);
      return;
    }
    this.service.updateRolesUser(this.rolesUser).subscribe({
      next: (res) => {
        if (res) {
          this.snotifyService.success(MessageConstants.UPDATED_OK_MSG, CaptionConstants.SUCCESS);
          this.modalAuthorizationUser?.hide();
        } else {
          this.snotifyService.error(MessageConstants.UPDATED_ERROR_MSG, CaptionConstants.ERROR);
        }
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UpdateErrorMsg'),
          this.translateService.instant('System.Caption.Error'));
      },
      complete: () => {
        this.modalAuthorizationUser?.hide();
      }
    });
  }

  ngOnDestroy(): void {
    this.modalService.remove(this.id);
    this.el.nativeElement.remove();
  }
}
