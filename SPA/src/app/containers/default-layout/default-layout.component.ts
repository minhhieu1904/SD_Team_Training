import { Component, OnInit } from '@angular/core';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { INavData } from '@coreui/angular';
import { ApplicationUser } from '@models/auth/application-user';
import { Role } from '@models/users';
import { AuthorService } from '@services/auth/auth.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Nav } from '../../../app/views/_nav';
@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html'
})
export class DefaultLayoutComponent implements OnInit {
  bsModalRef?: BsModalRef;
  public sidebarMinimized = false;
  public navItems: INavData[];
  user: ApplicationUser = JSON.parse(localStorage.getItem(LocalStorageConstants.USER)) ?? '{}';
  roles: Role[] = JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES)) ?? '{}';
  isAuthChangePassword: boolean = false;


  constructor(
    private authService: AuthorService,
    private modalService: BsModalService,
    private navItem: Nav
  ) {

  }
  async  ngOnInit() {
    this.navItems = await this.navItem.getNav();
  }

  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }

  logout() {
  }
}
