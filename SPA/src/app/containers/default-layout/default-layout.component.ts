import { ChangePasswordComponent } from './../change-password/change-password/change-password.component';
import { Nav } from './../../_nav';
import { Component, OnDestroy, OnInit, TemplateRef } from '@angular/core';
import { INavData } from '@coreui/angular';
import { AuthService } from '@services/auth/auth.service';
import { UserLoginParam } from '@models/auth/application-user';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgSnotifyService } from '@services/common/ng-snotify.service';
import { Role } from '@models/roleUsers';
import { CaptionConstants } from '@constants/message.enum';
import { UpdateInfoComponent } from '../update-info/update-info.component';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss'],
})
export class DefaultLayoutComponent implements OnInit, OnDestroy {
  bsModalRef?: BsModalRef;
  public sidebarMinimized = false;
  public navItems: INavData[];
  user: UserLoginParam =
    JSON.parse(localStorage.getItem(LocalStorageConstants.USER)) ?? '{}';
  roless: Role[] =
    JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES)) ?? '[]';
    isAuthChangePassword: boolean = false;
  constructor(
    private snotifyService: NgSnotifyService,
    private authService: AuthService,
    private modalService: BsModalService,
    private navItem: Nav
  ) {}

  async ngOnInit() {
    this.navItems = await this.navItem.getNav();
  }

  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }

  logout() {
    this.authService.logout();
    this.snotifyService.success(CaptionConstants.SUCCESS, CaptionConstants.SUCCESS);
  }
  onChangePassword(template: TemplateRef<any>) {
    this.bsModalRef = this.modalService.show(ChangePasswordComponent);
  }
  onChangeUser(template: TemplateRef<any>) {
    this.bsModalRef = this.modalService.show(UpdateInfoComponent);
  }
  ngOnDestroy(): void {}
}
