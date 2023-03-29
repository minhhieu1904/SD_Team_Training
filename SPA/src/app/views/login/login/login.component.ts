import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserLoginService } from '../../../_core/services/Login/UserLogin.service'
import { LocalStorageConstant } from '../../../_core/constants/localStorge.constants'
import { NgSnotifyService } from '@services/ng-snotify.service';
import { CaptionConstants, MessageConstants } from "@constants/message.enum";
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  user: any = {};
  constructor(private service: UserLoginService, private router: Router,
    private snotifyService: NgSnotifyService,
    private spinnerService: NgxSpinnerService) { }

  ngOnInit(): void {
  }

  login() { 
    console.log(this.user)
    this.spinnerService.show();
    this.service.login(this.user).subscribe({
      next: res => { 
        console.log(res);
        localStorage.setItem(LocalStorageConstant.Token, res.token);
        localStorage.setItem(LocalStorageConstant.User, JSON.stringify(res.user));
        localStorage.setItem(LocalStorageConstant.Role, JSON.stringify(res.user.roles));
        this.snotifyService.success(MessageConstants.LOGGED_IN, CaptionConstants.SUCCESS);
        
        this.router.navigate(['/default'])
        this.spinnerService.hide()
      },
      error: () => { 
        this.snotifyService.error(MessageConstants.LOGIN_FAILED, CaptionConstants.ERROR);
        this.spinnerService.hide();
      } 
    })
  }

}
