import { Injectable } from '@angular/core';
import { INavData } from '@coreui/angular';
import {LocalStorageConstants} from '@constants/local-storage.constants';
import {NavConstants} from '@constants/nav.constants';
import { RoleInformation } from '@models/auth/application-user';
@Injectable({ providedIn: 'root' })
export class Nav {
  nav: INavData[] = [];
  constructor() {}

  getNav() {
    this.nav = [];
    const navConstants = NavConstants;
    for (let i = 0; i < navConstants.length; i++) {
      const navParent: INavData = {
        name: `${i + 1}. ${navConstants[i].name}`,
        icon: navConstants[i].icon,
        url: navConstants[i].name.toLowerCase(),
        children: [],
      };
      this.nav.push(navParent);
    }

    const user_roles: RoleInformation[] =
      JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES)) || [];

    user_roles.forEach((roleItem, i) => {
      const positionIndex = +`${user_roles[i].position}`.charAt(0);
      const navItem: INavData = {
        name: roleItem.name,
        url: navConstants[positionIndex - 1].name.toLowerCase() + '/' + this.convertUrl(roleItem.name),
        class: 'menu-margin'
      };
      this.nav[  positionIndex - 1].children.push(navItem);
    });
    return this.nav;
  }

  convertUrl(str: string): string {
    const strConvert = str.toLowerCase().replace('/', ' ').split(' ');
    strConvert.shift();
    return strConvert.filter((x) => x !== '&').join('-');
  }
}
