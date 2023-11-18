import { SortNewreportByMidasProvideService } from './../../../../_core/services/report/sort-newreport-by-midas-provide.service';
import { Component, OnInit } from '@angular/core';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { IconButton } from '@constants/sd-team.utility';
import { InjectBase } from '@utilities/inject-base-app';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrl: './main.component.scss'
})
export class MainComponent extends InjectBase implements OnInit {
  sortDate: string = '';
  iconButton = IconButton;

  constructor(
    private service: SortNewreportByMidasProvideService
  ) { super() }


  ngOnInit(): void {
  }

  clearSearch() {
    this.sortDate = '';
  }

  exportExcel() {
    this.spinnerService.show();
    this.service.exportExcel(this.functionUtility.getDateFormat(new Date(this.sortDate)))
      .subscribe({
        next: res => {
          if (res.isSuccess) {
            const currentTime = new Date();
            const fileName = '4.5NewReportsByMidas_' +
              currentTime.getFullYear().toString() +
              (currentTime.getMonth() + 1) +
              currentTime.getDate() + currentTime.getHours() +
              (currentTime.getMinutes() + 1) + currentTime.getSeconds();;
            this.functionUtility.exportExcel(res.data, fileName);
            this.spinnerService.hide();
          } else {
            this.snotifyService.error(res.error, CaptionConstants.ERROR)
          }
        },
        error: (err) => {
          this.snotifyService.error(MessageConstants.UPDATED_ERROR_MSG, CaptionConstants.ERROR)
        },
      }).add(() => this.spinnerService.hide())
  }

}
