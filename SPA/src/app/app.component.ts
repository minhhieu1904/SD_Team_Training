import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

import { IconSetService } from '@coreui/icons-angular';
import { freeSet } from '@coreui/icons';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { LocalStorageConstants } from '@constants/local-storage.constants';
import { LangConstants } from '@constants/lang.constants';

@Component({
  // tslint:disable-next-line
  selector: 'body',
  template: `
    <router-outlet></router-outlet>
    <ng-snotify></ng-snotify>
    <ngx-spinner bdColor="rgba(51,51,51,0.8)" size="medium" color="#fff" type="ball-scale-multiple"></ngx-spinner>
  `,
  providers: [IconSetService],
})
export class AppComponent implements OnInit {
  lang: string = localStorage.getItem(LocalStorageConstants.LANG);
  constructor(
    private router: Router,
    public iconSet: IconSetService,
    private translate: TranslateService
  ) {
    // iconSet singleton
    iconSet.icons = { ...freeSet };
  }

  ngOnInit() {
    this.translate.addLangs([LangConstants.ZH, LangConstants.EN, LangConstants.ID, LangConstants.VI]);
    this.lang = this.lang ?? LangConstants.EN;
    this.translate.setDefaultLang(this.lang);
    this.translate.use(this.lang);
    localStorage.setItem(LocalStorageConstants.LANG, this.lang);

    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0);
    });
  }
}
