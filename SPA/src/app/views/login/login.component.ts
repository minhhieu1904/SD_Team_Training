import { LoginService } from './../../_core/services/login.service';
import { userLogin } from './../../_core/models/userLogin';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  user: any = {};
  constructor(private service: LoginService, private route: Router) { }

  ngOnInit(): void {
  }

  login() {
    this.service.login(this.user).subscribe({
      next: res => {
        console.log(res)
        localStorage.setItem('BottomQRCode_Token', res.token);
        localStorage.setItem('BottomQRCode_User', JSON.stringify(res.user));
        this.route.navigate(['maintain/authorization-setting'])
      },
      error: () => {}
    })
  }
}
