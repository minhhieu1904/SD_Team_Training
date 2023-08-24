import { Component, OnInit } from '@angular/core';
import { Router, NavigationEnd } from '@angular/router';

import { IconSetService } from '@coreui/icons-angular';
import { freeSet } from '@coreui/icons';
import { LocalStorageConstants } from '@constants/local-storage.constants';
//Thêm thư viện ngôn ngữ
import { TranslateService } from '@ngx-translate/core';
import { LangConstants } from '@constants/lang-constants';

@Component({
  //Chọn <body> trong HTML làm nơi hiển thị component
  selector: 'body',
  template: `
    <router-outlet></router-outlet>
    <ng-snotify></ng-snotify>
    <ngx-spinner bdColor="rgba(51,51,51,0.8)" size="medium" color="#fff" type="ball-scale-multiple"></ngx-spinner>
  `,
  providers: [IconSetService],
})
export class AppComponent implements OnInit {
  //Lấy giá trị ngôn ngữ
  lang: string = localStorage.getItem(LocalStorageConstants.LANG)

  constructor(
    private router: Router,
    public iconSet: IconSetService,
    private translate: TranslateService
  ) {
    iconSet.icons = { ...freeSet };
  }

  ngOnInit() {
    //Khởi tạo dịch vụ quản lý đa ngôn ngữ
    //Thêm danh sách ngôn ngữ hỗ trợ được cung cấp trong 'LangConstants'
    this.translate.addLangs([LangConstants.ZH, LangConstants.EN, LangConstants.ID, LangConstants.VI]);
    //Kiểm tra giá trị lang
    this.lang = this.lang ?? LangConstants.EN;
    //Đặt ngôn ngữ mặc định
    this.translate.setDefaultLang(this.lang);
    //Thiết lặp ngôn ngữ sử dụng cho dịch vụ
    this.translate.use(this.lang);
    //Lưu ngôn ngữ vào localStorage
    localStorage.setItem(LocalStorageConstants.LANG, this.lang);
    //Chỉnh sửa HTML ở index.html

    //Cuộn lên đầu trang khi chuyển trang
    this.router.events.subscribe((evt) => {
      if (!(evt instanceof NavigationEnd)) {
        return;
      }
      window.scrollTo(0, 0);
    });
  }
}
