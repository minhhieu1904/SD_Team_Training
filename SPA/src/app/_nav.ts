import { RoleInformation } from '@models/auth/application-user';
import { NavConstants } from './_core/constants/nav.constants';
import { Injectable } from '@angular/core';
import { INavData } from '@coreui/angular';
import { LocalStorageConstants } from '@constants/local-storage.constants';

@Injectable({ providedIn: 'root' })
export class Nav {
  navItems: INavData[] = [];
  constructor() {}

  getNav() {
    this.navItems = [];
    const navConstants = NavConstants;
    for (let i = 0; i < navConstants.length; i++) {
      const navParent: INavData = {
        name: `${i + 1}.${navConstants[i].name}`,
        icon: navConstants[i].icon,
        url: navConstants[i].name.toLowerCase(),
        children: [],
      };
      this.navItems.push(navParent);
    }
    const user_role: RoleInformation[] =
      JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES)) || [];
    user_role.forEach((roleItem, i) => {
      const positionIndex = +`${user_role[i].position}`.charAt(0);
      const nav: INavData = {
        name: roleItem.name,
        url:
          navConstants[positionIndex - 1].name.toLowerCase() +
          '/' +
          this.converUrl(roleItem.name),
        class: 'menu-margin',
      };
      this.navItems[positionIndex - 1].children.push(nav);
    });
    return this.navItems;
  }

  converUrl(str: string): string {
    const strConvert = str.toLowerCase().split(' ');
    strConvert.shift();
    return strConvert.filter((x) => x !== '&').join('-');
  }
}
