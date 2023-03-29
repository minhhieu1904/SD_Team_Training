import { Component, OnInit } from '@angular/core';
import { LocalStorageConstant } from '@constants/localStorge.constants';
import { ApplicationUser } from '@models/auth/auth';
import { TranslateService } from '@ngx-translate/core';
import { UserLoginService } from '@services/Login/UserLogin.service';
import { NgSnotifyService } from '@services/ng-snotify.service';

@Component({
  selector: 'app-header-container',
  templateUrl: './header-container.component.html',
  styleUrls: ['./header-container.component.scss']
})
export class HeaderContainerComponent implements OnInit {
  user: ApplicationUser = JSON.parse(localStorage.getItem(LocalStorageConstant.User)) ?? '{}';
  constructor(
    private translate: TranslateService,
    private snotifyService: NgSnotifyService,
    private service: UserLoginService
  ) { }

  ngOnInit(): void {
  }
  logout() {
    this.service.logout();
    this.snotifyService.success('Logout successful', 'Success');
  }
}
