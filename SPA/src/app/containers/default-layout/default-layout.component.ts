import { Component, OnDestroy, OnInit } from '@angular/core';
import { INavData } from '@coreui/angular';
import { Nav } from "../../views/_nav";
import { AuthorService } from '../../_core/services/auth/auth.service';
import { RoleInfomation, UserForLogged } from '@models/auth/application-user';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { LangConstants } from '../../_core/constants/lang.constants';
import { TranslateService } from '@ngx-translate/core';
import { takeUntil } from 'rxjs';
import { DestroyService } from '../../_core/services/common/destroy.service';
@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss'],
  providers: [DestroyService]
})
export class DefaultLayoutComponent implements OnInit, OnDestroy {
  public sidebarMinimized = false;
  public navItems: INavData[];
  langConstants: typeof LangConstants = LangConstants;
  rolesUser: RoleInfomation[] = JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES));
  user: UserForLogged = JSON.parse(localStorage.getItem(LocalStorageConstants.USER));
  lang: string = localStorage.getItem(LocalStorageConstants.LANG);
  constructor(
    private navItem: Nav,
    private authService: AuthorService,
    private translate: TranslateService,
    private destroyService: DestroyService) {
  }

  async ngOnInit() {
    if (!this.lang)
      localStorage.setItem(LocalStorageConstants.LANG, LangConstants.VI);
    this.navItems = await this.navItem.getNav(this.rolesUser);
    this.translate.onLangChange.pipe(takeUntil(this.destroyService.destroys$)).subscribe(async (res) => {
      this.navItems = await this.navItem.getNav(this.rolesUser);
    });
  }
  switchLang(lang: string) {
    this.translate.use(lang);
    localStorage.setItem(LocalStorageConstants.LANG, lang);
  }
  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }

  logout() {
    this.authService.logout();
  }

  ngOnDestroy(): void {
  }
}
