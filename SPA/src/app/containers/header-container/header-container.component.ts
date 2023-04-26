import { Component, OnInit } from '@angular/core';
import { LangConstants } from '@constants/lang-constant';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-header-container',
  templateUrl: './header-container.component.html',
  styleUrls: ['./header-container.component.scss']
})
export class HeaderContainerComponent implements OnInit {
  // account: string = sessionStorage.getItem(SessionStorageConstants.USER);
  lang: string = localStorage.getItem(LangConstants.LANG);
  langConstants: typeof LangConstants = LangConstants;

  constructor(
    private translate: TranslateService
  ) { }

  ngOnInit(): void {
    // if(!this.lang)
       // sessionStorage.setItem(SessionStorageConstants.LANG,LangConstants.EN)
  }
  switchLang(lang: string){
    this.translate.use(lang);
    localStorage.setItem(LangConstants.LANG,lang);
  }
}
