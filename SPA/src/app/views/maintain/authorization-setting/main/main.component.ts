import { IconButton } from './../../../../_core/constants/common.constants';
import { UsersService } from '@services/users.service';
import { Pagination } from './../../../../_core/utilities/pagination-utility';
import { UserRole } from './../../../../_core/models/roleUsers';
import { Users } from '@models/roleUsers';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgSnotifyService } from '@services/common/ng-snotify.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  data: Users[] = [];
  modalRef?: BsModalRef;
  userRole: UserRole = <UserRole>{
    account: '',
    listRoles: []
  };

  userAuthorization: Users = <Users>{};

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  };

  params: Users = <Users>{
    account: '',
    password: '',
    name: '',
    email: '',
    is_active: false,
  };

  iconButton = IconButton;

  constructor(
    private usersService: UsersService,
    private route: Router,
    private modalService: BsModalService,
    private snotify : NgSnotifyService
  ) {}

  ngOnInit(): void {
    this.getData()
  }

  getData(){
    this.usersService.getData(this.params,  this.pagination).subscribe({
      next: (res) => {
        this.data = res.result;
        this.pagination = res.pagination;
      },
      error: () => {},
      complete: () => {}
    })
  }

  search() {
    this.pagination.pageNumber === 1
      ? this.getData()
      : (this.pagination.pageNumber = 1);
  }

  clear() {
    this.params = {} as Users;
    this.getData();
  }

  addNew() {
    this.route.navigate(['maintain/authorization-setting/add'])
  }

  update(item: Users) {
    // ban du lieu
    // goi toi servcie chứa BehaviorSubject
    this.usersService.dataSources.next(item);
    this.route.navigate(['maintain/authorization-setting/update'])
  }

  pageChanged(e:any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  openModal(template: TemplateRef<any>, account: string, name:string) {
    this.modalRef = this.modalService.show(template, {
      ariaDescribedby: 'my-modal-description',
      ariaLabelledBy: 'my-modal-title'
    });
    // giờ gọi hàm get quyền theo account bên aPI
    this.usersService.getAuthorizeByUser(account).subscribe({
      next: (res) =>{
        this.userRole = res
        this.params.name = name;

      },
      error:() => {
        this.snotify.error('GetData Fail', 'Error')
      }
    })
  }

  save() {
    this.usersService.updateAuthorizeByUser(this.userRole).subscribe({
      next: (res) => {
        if(res.isSuccess)
        this.snotify.success('Update Success', 'Success')
        this.modalRef.hide()
      },
      error: () => {
        this.snotify.error('Update Fail', 'Error')
      }
    })
  }
}
