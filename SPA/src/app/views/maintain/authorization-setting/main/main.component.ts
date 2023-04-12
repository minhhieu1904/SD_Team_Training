import { Component, OnInit, TemplateRef } from '@angular/core';
import { AuthorizationSettingService } from '../../../../_core/services/maintain/authorization-setting.service'
import { InjectBase } from '@utilities/inject-base-app';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { User, UserParam } from '@models/maintain/role';
import { IconButton } from '@constants/common.constants';
import { List_RoleUserParam } from '@models/maintain/list_RoleUserParam';
import { Pagination } from '@utilities/pagination-utility';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
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
  ) { super(); }

  ngOnInit(): void {
    this.getData();
  }
  getData() {
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param.account).subscribe({
      next: (result) => {
        this.pagination = result.pagination;
        this.data = result.result;
        this.spinnerService.hide();
      },
    });
  }
  add(){
    this.router.navigate(['maintain/authorization-setting/add']);
  }
  clear() {
    this.param.account = '';
    this.param.name = '';
    this.getData();
  }
  update(user: User){
    this.router.navigate([`maintain/authorization-setting/edit/${user.account}`])
  }
  search(){
    this.pagination.pageNumber = 1;
    this.getData();
  }
  pageChanged(e: any){
    this.pagination.pageNumber = e.page;
    this.getData();
  }
  openModal(users: User, tmp: TemplateRef<any>) {
    this.spinnerService.show();
    this.user.account = users.account;
    this.user.name = users.name;
    this.service.getAuthorizeByUser(users.account).subscribe({
      next: result => {
        this.spinnerService.hide();
        this.list_RoleUserParam = result;
        this.modalRef = this.modalService.show(tmp);
      },error: () => this.spinnerService.hide()
    });
  }

  closeModal(modalId?: number) {
    this.modalService.hide(modalId);
  }
  updateAuthorizeByUser() {
    this.service.updateAuthorizeByUser(this.list_RoleUserParam).subscribe({
      next: (result) => {
        console.log(result);
        this.closeModal();
      },
    });
  }
}
