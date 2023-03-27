import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../../_core/services/auth.service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  user: any = {};
  constructor(private service: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  login() { 
    console.log(this.user)
    this.service.login(this.user).subscribe({
      next: res => { 
        console.log(res);
        localStorage.setItem('BottomQRCode_Token', res.token);
        localStorage.setItem('BottomQRCode_User', JSON.stringify(res.user));
        this.router.navigate(['/authorization-setting'])
      },
      error: () => { 
      } 
    })
  }

}
