import { Component, OnInit, TemplateRef } from '@angular/core';
import { List_RoleUserParam } from '@models/maintain/list_RoleUserParam';
import { User } from '@models/maintain/user';
import { InjectBase } from '@utilities/inject-base-app';
import { Pagination } from '@utilities/pagination-utility';
import { AuthorizationSettingService } from '../../../../_core/services/maintain/authorization-setting.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { UserParam } from '@models/maintain/userParam';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { url } from '@constants/url.constants';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  data: User[] = [];
  user: User = <User>{};
  modalRef?: BsModalRef;
  iconButton: typeof IconButton = IconButton;
  list_RoleUserParam: List_RoleUserParam = <List_RoleUserParam>{
    account: '',
    listRoles: [],
  };

  param: UserParam = <UserParam>{
    account: '',
    name: '',
    email: '',
  };

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };
  constructor(
    private service: AuthorizationSettingService,
    private modalService: BsModalService
  ) {
    super();
  }

  ngOnInit(): void {
    this.getDataPagination();
  }

  getDataPagination() {
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param.account).subscribe({
      next: (result) => {
        this.pagination = result.pagination;
        this.data = result.result;
        this.spinnerService.hide();
      },
      error: () => {
        this.spinnerService.hide();
        this.snotifyService.error(
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
        );
      },
    });
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getDataPagination();
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getDataPagination();
  }

  add() {
    this.router.navigate([`${url.maintain.authorization_setting}/add`]);
  }

  update(user: User) {
    this.router.navigate([
      `${url.maintain.authorization_setting}/edit/${user.account}`,
    ]);
  }

  clear() {
    this.user.account = '';
    this.user.email = '';
    this.user.name = '';
    this.user.password = '';
    this.getDataPagination();
  }

  openModal(users: User, tmp: TemplateRef<any>) {
    debugger;
    this.spinnerService.show();
    this.user.account = users.account;
    this.user.name = users.name;
    this.service.getAuthorizeByUser(users.account).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.list_RoleUserParam = result;
        this.modalRef = this.modalService.show(tmp);
      },
      error: () => this.spinnerService.hide(),
      complete: () => this.spinnerService.hide(),
    });
  }

  closeModal(modalId?: number) {
    this.modalService.hide(modalId);
  }

  updateAuthorizeByUser() {
    this.service.updateAuthorizeByUser(this.list_RoleUserParam).subscribe({
      next: () => {
        this.closeModal();
      },
    });
  }
}
