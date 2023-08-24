import { firstValueFrom } from 'rxjs';
import { Injectable } from '@angular/core';
import { INavData } from '@coreui/angular';
import { RoleInfomation } from './_core/models/auth/auth';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { NavConstants } from '@constants/nav.constants';
//Cài đặt '@ngx-translate/core' và 'import TranslateModule' vào AppModule 'app.module.ts'
//Tạo thư mục 'assets/i18n' và thêm Tệp dịch
import { TranslateService } from '@ngx-translate/core';
@Injectable({ providedIn: 'root' })
export class Nav {
  nav: INavData[] = [];
  constructor(
    private translateService: TranslateService
  ) { }

  async getNav(roles: RoleInfomation[]): Promise<INavData[]> {
    this.nav = [];
    const navConstants = NavConstants;

    await Promise.all(navConstants.map(async (navConstant, i) => {
      const navParent: INavData = {
        name: `${i + 1}. ${await firstValueFrom(this.translateService.get(navConstants[i].variant))}`,
        icon: navConstant.icon,
        url: navConstant.name.toLowerCase(),
        children: [],
      };
      this.nav.push(navParent);
    }));

    const user_roles: RoleInfomation[] =
      JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES)) || [];

    await Promise.all(user_roles.map(async (roleItem, i) => {
      const positionIndex = +`${user_roles[i].position}`.charAt(0);
      const navItem: INavData = {
        name: await firstValueFrom(this.translateService.get(`Roles.Menu_Child.${roles[i].unique}`)),
        url: navConstants[positionIndex - 1]?.name.toLowerCase() + '/' + this.convertUrl(roleItem.name),
        class: 'menu-margin'
      };
      this.nav[positionIndex - 1]?.children.push(navItem);
    }));
    return this.nav;
  }

  convertUrl(str: string): string {
    const strConvert = str.toLowerCase().replace(/\/|&/g, ' ').trim().split(/\s+/);
    strConvert.shift();
    return strConvert.join('-');
  }
}
//Sau đó triển khai 'Nav' Service 'nav.service.ts' tại 'src/app'
