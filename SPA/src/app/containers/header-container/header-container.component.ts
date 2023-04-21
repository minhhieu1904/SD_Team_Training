import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LangConstant } from '@constants/lang.constants';
import { LocalStorageConstant } from '@constants/localStorge.constants';
import { TranslateService } from '@ngx-translate/core';
@Component({
  selector: 'app-header-container',
  templateUrl: './header-container.component.html',
  styleUrls: ['./header-container.component.scss'],
})
export class HeaderContainerComponent implements OnInit {
  account: string = 'Administrator';
  langConstant = LangConstant
  constructor(private translate: TranslateService, private router: Router) {}
  lang: string = localStorage.getItem(LocalStorageConstant.Lang) ?? this.langConstant.EN;
  ngOnInit(): void {
    this.translate.use(this.lang)
  }
  logout() {
    localStorage.clear();
    this.router.navigate(['/login']);
  }

  switchLang(lang:string){
    this.translate.use(lang)
  }
}
