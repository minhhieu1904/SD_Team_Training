import { Nav } from './../../_nav';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { INavData } from '@coreui/angular';
import { AuthService } from '@services/auth/auth.service';
import { UserLoginParam } from '@models/auth/application-user';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { NgSnotifyService } from '@services/common/ng-snotify.service';
import { Role } from '@models/maintain/roleUsers';
import { CaptionConstants } from '@constants/message.enum';

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

  ngOnDestroy(): void {}
}
