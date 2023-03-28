import { Component, OnInit } from '@angular/core';
import { INavData } from '@coreui/angular';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { Nav } from '../../_nav';
@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
})
export class DefaultLayoutComponent implements OnInit {
  BsModalRef?: BsModalRef;
  public sidebarMinimized = false;
  public navItems: INavData[];
  constructor(private navItem: Nav) {}
  async ngOnInit() {
    this.navItems = await this.navItem.getNav();
  }
  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }
}
