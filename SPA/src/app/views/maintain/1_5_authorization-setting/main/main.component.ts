import { AuthorizationSettingService } from '@services/maintain/authorization-setting.service';
import { AuthorizationParam } from '@models/maintain/authorization-setting';
import { IconButton } from '@constants/common.constants';
import { Pagination } from '@utilities/pagination-utility';
import { InjectBase } from '@utilities/inject-base-app';
import { Component, OnInit } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { Users } from '@models/common/users';
import { ModalService } from '@services/common/modal.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  param: AuthorizationParam = <AuthorizationParam>{}
  user: Users = <Users>{}
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  }
  data: Users[] = [];
  title: string = '';
  iconButton = IconButton;
  constructor(
    private service: AuthorizationSettingService,
    private modalService: ModalService
  ) {
    super();
  }

  ngOnInit() {
    this.getData()
  }

  getData() {
    this.spinnerService.show();
    this.service.getData(this.param, this.pagination).subscribe({
      next: (res) => {
        this.data = res.result;
        this.pagination = res.pagination;
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(
          this.translateService.instant('System.Message.SystemError'),
          this.translateService.instant('System.Caption.Error'))
        this.spinnerService.hide()
      },
    })
  }

  pageChanged(event: PageChangedEvent) {
    this.pagination.pageNumber = event.page;
    this.getData();
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getData();
  }

  clear() {
    this.param.account = '';
    this.param.name = '';
    this.pagination.pageNumber = 1;
    this.getData();
  }

  //Mở form modal
  openModal(id: string, user: Users) {
    this.service.layoutSource.next(user);
    this.modalService.open(id);
  }

  //Thêm mới
  add() {
    this.user = <Users>{ type: 'add', is_active: true }
    this.service.sendDataToOtherComponent(this.user);
    this.router.navigate(['maintain/authorization-setting/add']);
  }

  //Edit
  edit(data: Users) {
    this.user = { ...data }
    this.user.type = 'edit';
    this.service.sendDataToOtherComponent(this.user);
    this.router.navigate(['maintain/authorization-setting/edit']);
  }
}
