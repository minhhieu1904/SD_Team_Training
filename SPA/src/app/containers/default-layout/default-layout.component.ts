import { Component, OnInit } from '@angular/core';
import { LocalStorageConstant } from '@constants/localStorge.constants';
import { INavData } from '@coreui/angular';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ApplicationUser } from "../../_core/models/auth/auth";
import { Nav } from "../../views/_nav";
@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent implements OnInit {
  BsModalRef?: BsModalRef;
  public sidebarMinimized = false;
  public navItems: INavData[];
  user: ApplicationUser = JSON.parse(localStorage.getItem(LocalStorageConstant.User)) ?? '{}';
  isAuthChangePassword: boolean = false;
  constructor(
    private navItem: Nav
  ) {
  }
  async ngOnInit() {
    this.navItems = await this.navItem.getNav();
  }
  toggleMinimized(e){
    this.sidebarMinimized = e;
  }
  logout() {
  }
}
