import { Component, OnInit } from '@angular/core';
import { LocalStorageConstant } from '@constants/localStorge.constants';
import { ApplicationUser } from '@models/auth/auth';
import { TranslateService } from '@ngx-translate/core';
import { UserLoginService } from '@services/Login/UserLogin.service';
import { NgSnotifyService } from '@services/ng-snotify.service';
import { LangConstant } from '@constants/lang.constants';

@Component({
  selector: 'app-header-container',
  templateUrl: './header-container.component.html',
  styleUrls: ['./header-container.component.scss']
})
export class HeaderContainerComponent implements OnInit {
  user: ApplicationUser = JSON.parse(localStorage.getItem(LocalStorageConstant.User)) ?? '{}';
  langConstant = LangConstant
  lang: string = localStorage.getItem(LocalStorageConstant.Lang) ?? this.langConstant.EN;
  constructor(
    private translate: TranslateService,
    private snotifyService: NgSnotifyService,
    private service: UserLoginService
  ) { }

  ngOnInit(): void {
    this.translate.use(this.lang)
  }
  logout() {
    this.service.logout();
    this.snotifyService.success('Logout successful', 'Success');
  }
  switchLang(lang:string){
    this.translate.use(lang)
  }
}
