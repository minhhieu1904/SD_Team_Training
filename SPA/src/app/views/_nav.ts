import { Injectable } from '@angular/core';
import { INavData } from '@coreui/angular';
import { LocalStorageConstants } from '../_core/constants/local-storage.constants';
import { RoleInformation } from '../_core/models/auth/application-user';
import { RolesConstants } from '../_core/constants/roles.constants';
@Injectable({ providedIn: 'root' })
export class Nav {
  user_roles: RoleInformation[] = JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES)!) || [];
  navItems: INavData[] = [];
  constructor() { }

  getNav() {
    let roles = this.user_roles.map(x => x.unique.trim());
    let rolesConstants = RolesConstants;
    this.navItems = [];
    rolesConstants.forEach(nav => {
      debugger
      let navItem: INavData = { ...nav, children: [] };
      nav.children.forEach(childNav => {
        // nếu position 
        if (roles.includes(childNav.role)) {
          navItem.children!.push(childNav)
        }

      });
      this.navItems.push(navItem);
    });

    return this.navItems;
  }
}

