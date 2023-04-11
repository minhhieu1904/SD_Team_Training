import { Component, OnInit, TemplateRef } from '@angular/core';
import { InjectBase } from '@utilities/inject-base-app';
import { AuthorizationSettingService } from './../../../../_core/services/authorization-setting.service';
import { User, UserParam, UserRole } from '@models/maintain/user';
import { IconButton } from '@constants/common.constants';
import { Pagination } from './../../../../_core/utilities/pagination-utility';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';

@Component({
  selector: 'app-user-main',
  templateUrl: './user-main.component.html',
  styleUrls: ['./user-main.component.scss'],
})
export class UserMainComponent extends InjectBase implements OnInit {
  constructor(
    private service: AuthorizationSettingService,
    private modalService: BsModalService
  ) {
    super();
  }

  param: UserParam = <UserParam>{
    account: '',
    password: '',
    email: '',
    name: '',
    update_by: '',
    is_active: 2,
  };

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  userRole: UserRole = <UserRole>{
    account: '',
    listRoles: [],
  };

  userAuthorize: User = <User>{};
  modalRef?: BsModalRef | null;
  data: User[] = [];
  icon = IconButton;

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.spinnerService.show();
    this.service.getAll(this.pagination, this.param).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.data = result.result;
        this.pagination = result.pagination;
      },
    });
  }

  pageChanged(e) {
    this.pagination.pageNumber = e.page;
    this.getAll();
  }

  addNew() {
    this.router.navigate(['/maintain/authorization-setting/add']);
  }
  clear() {
    this.param.account = '';
    this.param.email = '';
    this.param.name = '';
    this.param.update_by = '';
    this.param.is_active = 2;
    this.pagination.pageNumber = 1;

    this.getAll();
  }
  search() {
    this.pagination.pageNumber = 1;

    this.getAll();
  }

  edit(model: User) {
    this.param = {
      account: model.account,
      password: model.password,
      email: model.email,
      name: model.name,
      is_active: model.is_active,
      update_by: model.update_by,
    };

    this.service.dataSource.next(this.param);
    this.router.navigate(['/maintain/authorization-setting/edit']);
  }

  getAllRoleByAccount(account: string) {
    this.spinnerService.show();
    this.service.getAllRoleByAccount(account).subscribe({
      next: (result) => {
        this.spinnerService.hide();
        this.userRole = result;
      },
    });
  }

  block(model: User, template: TemplateRef<any>) {
    this.getAllRoleByAccount(model.account);
    this.userAuthorize.account = model.account;
    this.userAuthorize.name = model.name;

    this.modalRef = this.modalService.show(template);
  }

  closeModal(modalId?: number) {
    this.modalService.hide(modalId);
  }

  save() {
    this.spinnerService.show();
    this.service.updateRoleByAccount(this.userRole).subscribe({
      next: (resutl) => {
        this.spinnerService.hide();
        if (resutl.isSuccess) {
          this.snotifyService.success(
            MessageConstants.UPDATED_OK_MSG,
            CaptionConstants.SUCCESS
          );
          this.closeModal();
        } else {
          this.snotifyService.error(
            MessageConstants.UPDATED_ERROR_MSG,
            CaptionConstants.ERROR
          );
        }
      },
      error: (err) => {
        this.snotifyService.error(
          MessageConstants.UN_KNOWN_ERROR,
          CaptionConstants.ERROR
        );
      },
    });
  }
}
