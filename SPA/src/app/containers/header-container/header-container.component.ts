import { Component, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { UserLoginParam } from '@models/maintain/application-user';
import { LocalStorageConstants } from '@constants/localStorge.constants';
import { UserLoginService } from '@services/maintain/user-login.service';
import { NgSnotifyService } from '@services/common/ng-snotify.service';
import { CaptionConstants } from '@constants/message.enum';
@Component({
  selector: 'app-header-container',
  templateUrl: './header-container.component.html',
  styleUrls: ['./header-container.component.scss']
})
export class HeaderContainerComponent implements OnInit {
  account: string = 'Administrator';
  user: UserLoginParam = JSON.parse(localStorage.getItem(LocalStorageConstants.USER)) ?? '{}';
  constructor(
    private service: UserLoginService,
    private snotifyService: NgSnotifyService,
    private translate: TranslateService
  ) { }
  logout() {
    this.service.logout();
    this.snotifyService.success(CaptionConstants.SUCCESS, CaptionConstants.SUCCESS);
  }

  ngOnInit(): void {
  }
}
