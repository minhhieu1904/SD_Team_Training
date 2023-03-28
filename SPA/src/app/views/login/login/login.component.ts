import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserLoginService } from '../../../_core/services/Login/UserLogin.service'
import { LocalStorageConstant } from '../../../_core/constants/localStorge.constants'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  user: any = {};
  constructor(private service: UserLoginService, private router: Router) { }

  ngOnInit(): void {
  }

  login() { 
    console.log(this.user)
    this.service.login(this.user).subscribe({
      next: res => { 
        console.log(res);
        localStorage.setItem(LocalStorageConstant.Token, res.token);
        localStorage.setItem(LocalStorageConstant.User, JSON.stringify(res.user));
        localStorage.setItem(LocalStorageConstant.Role, JSON.stringify(res.user.roles));
        this.router.navigate(['maintain/authorization-setting'])
      },
      error: () => { 
      } 
    })
  }

}
