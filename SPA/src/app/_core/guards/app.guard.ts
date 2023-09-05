import { Injectable } from '@angular/core';
import { CanLoad, Route, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { LocalStorageConstants } from "@constants/local-storage.constants";
import { RoleInfomation } from "@models/auth/auth";

@Injectable({
  providedIn: 'root'
})
export class AppGuard implements CanLoad {
  constructor(private router: Router) { }

  //Hàm canLoad được gọi khi trình duyệt cố gắng tải một route
  canLoad(route: Route): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    //Bước 1: Lấy giá trị 'role' từ route data
    let role = route.data['role'];

    //Bước 2: Trích xuất danh sách vai trò của người dùng từ localStorage
    let roleOfUser: RoleInfomation[] = JSON.parse(localStorage.getItem(LocalStorageConstants.ROLES));

    //Bước 3: Kiểm tra xem vai trò của người dùng có khớp với 'role' trong route không
    let checkRole = roleOfUser.map(x => x.unique).some(x => x.trim() === role?.trim());

    //Bước 4: Nêu vai trò khớp, cho phép tải tuyến đường, ngược lại chuyển hướng đến '/dashboard'
    if (checkRole) {
      return true;  //Cho phép tải route
    } else {
      this.router.navigate(['/dashboard']); //Chuyển hướng đến '/dashboard'
      return false; //Ngăn chặn tải route
    }
  }
}
