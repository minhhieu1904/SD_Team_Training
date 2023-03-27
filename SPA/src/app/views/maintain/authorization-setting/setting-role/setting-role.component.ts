import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { IconButton } from '@constants/sd-team.utility';
import { Users } from '@models/common/users';
import { RolesUserStatus } from '@models/maintain/authorization-setting';
import { AuthorizationSettingService } from '@services/main/authorization-setting.service';
import { ModalService } from '@services/common/modal.service';
import { InjectBase } from '@utilities/inject-base-app';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-setting-role',
  templateUrl: './setting-role.component.html',
  styleUrls: ['./setting-role.component.css']
})
export class SettingRoleComponent extends InjectBase implements OnInit {
  @Input() id: string;
  userAuthorization: Users = <Users>{};
  IconButton = IconButton;
  @ViewChild('modalAuthorizationUser', { static: false }) modalAuthorizationUser?: ModalDirective;
  rolesUser: RolesUserStatus[] = [];
  rolesUserConst: RolesUserStatus[] = [];
  constructor(private modalService: ModalService,
    private el: ElementRef,
    private authorSettingService: AuthorizationSettingService) {
    super();
  }

  ngOnInit() {
    console.log(this.rolesUser);
    if (!this.id) {
      return;
    }
    this.modalService.add(this);
    this.authorSettingService.userAuthorization.asObservable().subscribe(res => {
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
    this.authorSettingService.getRoleUser(account).subscribe({
      next: (res) => {
        console.log("aaaaaaa:", res);
        this.rolesUserConst = res;
        this.rolesUser = JSON.parse(JSON.stringify(this.rolesUserConst));
      },
      error: () => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
      },
    })
  }

  saveUpdateAuthorUser() {
    let checkChangeRole = JSON.stringify(this.rolesUserConst) === JSON.stringify(this.rolesUser);
    if (checkChangeRole) {
      return this.snotifyService.error('Nothing has changed, Please change role of user!!!', CaptionConstants.ERROR)
    }
    this.authorSettingService.updateAuthorUser(this.rolesUser).subscribe({
      next: (res) => {
        if (res) {
          this.snotifyService.success(
            this.translateService.instant('System.Message.UpdateOKMsg'),
            this.translateService.instant('System.Caption.Success'))
          this.modalAuthorizationUser.hide();
        } else {
          this.snotifyService.error(
            this.translateService.instant('System.Message.UpdateErrorMsg'),
            this.translateService.instant('System.Caption.Error'));
        }
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UpdateErrorMsg'),
          this.translateService.instant('System.Caption.Error'));
      }
    })
  }

  // remove self from modal service when directive is destroyed
  ngOnDestroy(): void {
    this.modalService.remove(this.id);
    this.el.nativeElement.remove();
  }

}
