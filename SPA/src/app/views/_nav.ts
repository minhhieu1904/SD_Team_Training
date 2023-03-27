import { Injectable } from '@angular/core';
import { INavData } from '@coreui/angular';
import { RoleInfomation } from '@models/auth/application-user';
import { FunctionUtility } from '@utilities/function-utility';
import { NavConstants } from '@constants/nav.constants';
import { TranslateService } from '@ngx-translate/core';
import { firstValueFrom } from 'rxjs';
@Injectable({ providedIn: 'root' })
export class Nav {
  navItems: INavData[] = [];
  constructor(private functionUtility: FunctionUtility,
    private translateService: TranslateService) {
  }
  async getNav(roles: RoleInfomation[]): Promise<INavData[]> {
    this.navItems = [];
    const navConstants = NavConstants;
    for (let i = 0; i < navConstants.length; i++) {
      let navParent: INavData = {
        name: `${i + 1}. ${await firstValueFrom(this.translateService.get(navConstants[i].variant))}`,
        icon: navConstants[i].icon,
        url: navConstants[i].name.toLowerCase(),
        children: []
      }
      this.navItems.push(navParent);
    }
    let routerParents = navConstants.map(x => x.name.toLowerCase());
    if (roles && roles.length > 0) {
      for (let i = 0; i < roles.length; i++) {
        let positionIndex = +(`${roles[i].position}`.charAt(0));
        let navItem: INavData = {
          name: await firstValueFrom(this.translateService.get(`Roles.Menu_Child.${roles[i].unique}`)),
          url: roles[i].unique === "BQRC.QRCodeWIPReport" ? 'report/qrcode-wip-report' : this.functionUtility.convertUrlMenu(roles[i].unique, positionIndex - 1, routerParents),
          class: 'menu-margin'
        };
        this.navItems[positionIndex - 1].children.push(navItem);
      }
    }
    return this.navItems;
  }
}
