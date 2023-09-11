import { Component, OnInit } from '@angular/core';
import { INavData } from '@coreui/angular';
import { Role } from '@models/maintain/user';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { Nav } from './../../_nav';
import { UserForLogged, UserLoginParam } from '@models/auth/application-user';
import { AuthService } from '@services/auth/auth.service';
import { InjectBase } from '@utilities/inject-base-app';
import { CaptionConstants } from '@constants/message.enum';
import { LangConstants } from '@constants/lang-constants';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
})
export class DefaultLayoutComponent extends InjectBase implements OnInit {
  bsModalRef?: BsModalRef;
  public sidebarMinimized = false;
  public navItems: INavData[];
  langConstants: typeof LangConstants = LangConstants;
  user: UserForLogged =
    JSON.parse(localStorage.getItem(LocalStorageConstants.USER)) ?? '{}';

  constructor(private navItem: Nav, private service: AuthService, private translate: TranslateService,) { super() }
  async ngOnInit() {
    this.navItems = await this.navItem.getNav();

   
  }

  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }

  switchLang(lang: string) {
    this.translate.use(lang == LangConstants.ZH_TW ? 'zh' : lang);
    localStorage.setItem(LocalStorageConstants.LANG, lang);
  }

  logout() {
    this.service.logout();
    this.snotifyService.success(CaptionConstants.SUCCESS, CaptionConstants.SUCCESS);
  }

}
