import { Component, OnInit } from '@angular/core';
import { Users } from '@models/common/users';
import { AuthorizationSettingService } from '../../../../_core/services/main/authorization-setting.service';
import { Pagination } from '@utilities/pagination-utility';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { IconButton } from '@constants/sd-team.utility';
import { ModalService } from '../../../../_core/services/common/modal.service';
import { InjectBase } from '@utilities/inject-base-app';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  IconButton = IconButton;
  users: Users[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  };

  userType: {
    data: Users,
    type: true;
  }
  constructor(
    private authorSettingService: AuthorizationSettingService,
    private modalService: ModalService) {
    super();
  }

  ngOnInit() {
    this.getDataUsers();
  }

  getDataUsers() {
    this.spinnerService.show();
    this.authorSettingService.getDataUsers(this.pagination).subscribe({
      next: (res) => {
        this.users = res.result;
        this.pagination = res.pagination;
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.UnknowError'),
          this.translateService.instant('System.Caption.Error'));
        this.spinnerService.hide();
      },
      complete: () => {
        this.spinnerService.hide();
      }
    })
  }
  pageChanged(event: PageChangedEvent) {
    this.pagination.pageNumber = event.page;
    this.getDataUsers();
  }

  // ---------------------------------------- Open Modal Authorization----------------------------------------//
  openModal(id: string, user: Users) {
    this.authorSettingService.userAuthorization.next(user);
    this.modalService.open(id);
  }


  ////////////////////////////////////////////////////Send Data//////////////////////////////////////////////////////////////
  dataPass?: Users = <Users>{};
  passDataEdit(data: Users) {
    this.dataPass = { ...data };
    this.dataPass.type = 'edit';
    this.authorSettingService.sendDataToOtherComponent(this.dataPass);
    this.router.navigate(['maintain/authorization-setting/edit']);
  }

  passDataAdd() {
    this.dataPass = <Users>{ type: 'add', is_active: true };
    this.authorSettingService.sendDataToOtherComponent(this.dataPass);
    this.router.navigate(['maintain/authorization-setting/add']);
  }
}
