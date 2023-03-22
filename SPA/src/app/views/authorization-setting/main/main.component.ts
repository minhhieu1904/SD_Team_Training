import { Component, OnInit, TemplateRef } from '@angular/core';
import { authorizationSetting, UserRole, Users } from '@models/users';
import { Pagination } from '@utilities/pagination-utility';
import { IconButton } from '@constants/common.constants';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthorizationSettingService } from '../../../_core/services/authorization-setting.service';
import { InjectBase } from '../../../_core/utilities/inject-base-app';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent extends InjectBase implements OnInit {
  data: Users[] = [];
  modalRef?: BsModalRef;
  iconButton: typeof IconButton = IconButton;
  // Danh sách quyền theo User
  userRole: UserRole = <UserRole>{
    account: '',
    listRoles: []
  };

  userAuthorize: Users = <Users>{ };



  pagination: Pagination = <Pagination>{
    pageNumber: 1, 
    pageSize: 10
  };
  param: authorizationSetting = <authorizationSetting>{
    account: '',
    name: '',
    email:''
  }
  constructor( private service: AuthorizationSettingService,
    private modalService: BsModalService) { super()}


  ngOnInit() {
    this.getData();
  }

  getData() { 
    this.service.getData(this.pagination,this.param).subscribe({
      next: res => { 
        this.data = res.result;
        this.pagination = res.pagination;
      },
      error: () => { 
      }
    })
  }

  search() { 
    this.pagination.pageNumber = 1;
    this.getData();
  }

  add(){
    this.router.navigate(["authorization-setting/add"])
  }

  pageChanged(e: any) { 
    this.pagination.pageNumber = e.page;
    this.getData()

  }

  edit(users: Users){
    // truyền đi cùng dữ liệu
    this.router.navigate([`authorization-setting/edit/${users.account}`])
  }
  clear(){
    this.param.account='';
    this.param.name=''
    this.search()
  }

  getAuthorizeByAccount(account: string) {
    this.spinnerService.show();
    this.service.getAuthorizeByUser(account).subscribe({
      next: (result) => {
        console.log(result)
        this.userRole = result;
        console.log(`quyen user ${this.userRole.account}`, this.userRole);

        this.spinnerService.hide();
      },
      error: (err) => {
        this.spinnerService.hide();
        console.log(err);
      },
      complete: () => {
        this.spinnerService.hide();
      },
    });
  }
  openModal(user: Users, template: TemplateRef<any>) {
    this.getAuthorizeByAccount(user.account);
    console.log(user);
    // gọi lên API lấy danh sách quyền của User đó bao gồm những tin:
    this.userAuthorize.account = user.account;
    this.userAuthorize.name = user.name;

    this.modalRef = this.modalService.show(template);
  }


  closeModal(modalId?: number){
    this.modalService.hide(modalId);
  }
  save()
  {
    // console.log('abc',this.userRole.listRoles.filter(x => x.isCheck));

    this.service.updateAuthorizeByUser(this.userRole).subscribe({
      next: res => {
        console.log(res)
        this.closeModal();
      },
      error: (err) =>{
        console.log('save not successfully', err)
      }
    })
  }
}
