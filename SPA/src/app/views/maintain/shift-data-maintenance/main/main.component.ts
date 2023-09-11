import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Route, Router } from '@angular/router';
import { MS_Shift, ShiftDataMaintainParam } from '@models/maintain/msshift';
import { Pagination } from '@utilities/pagination-utility';
import { IconButton } from '../../../../_core/constants/common.constants';
import { ShiftDataMaintenanceService } from '../../../../_core/services/shift-data-maintenance.service';
import { InjectBase } from '../../../../_core/utilities/inject-base-app';
import { CaptionConstants, MessageConstants } from '../../../../_core/constants/message.enum'
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
// import { MS_Shift } from '@models/maintain/shift-data-maintenance';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  @ViewChild('inputRef') inputRef: ElementRef<HTMLInputElement>;
  param: ShiftDataMaintainParam = <ShiftDataMaintainParam>{};
  data: MS_Shift[] = [];
  pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };

  extensions: string = '.xls, .xlsm, .xlsx';
  iconButton = IconButton;

  constructor(
    private service: ShiftDataMaintenanceService,
  ) {super()}

  ngOnInit() {
    this.getData();
  }

  getData() {
    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: (res) => {
        this.data = res.result;
        this.pagination = res.pagination;
        console.log(this.data);

        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.ERROR);
        this.spinnerService.hide();
      },
    });
  }

  add() {
    this.router.navigate(['maintain/shift-data-maintenance/add']);
  }

  clear() {
    this.param.shift = '';
    this.param.shiftName = '';
    this.getData();
  }

  delete(item : MS_Shift){
    this.snotifyService.confirm(MessageConstants.CONFIRM_DELETE, CaptionConstants.WARNING, () => {
        this.spinnerService.show();
        console.log(item);

        this.service.delete(item).subscribe({
            next: (res) => {
              if (res.isSuccess) {
                this.snotifyService.success(MessageConstants.DELETED_OK_MSG, CaptionConstants.SUCCESS);
                this.clear();
              }
              else
                this.snotifyService.error(MessageConstants.DELETED_ERROR_MSG, CaptionConstants.ERROR);
            },
            error: () => this.snotifyService.error(MessageConstants.UN_KNOWN_ERROR, CaptionConstants.SUCCESS),
            complete: () => this.spinnerService.hide()
          });
      }
    )
  }

  edit(item: MS_Shift) {
    this.param = {
      shift: item.shift,
      shiftName: item.shiftName,
    };
    this.service.dataSource.next(this.param);
    this.router.navigate(['maintain/shift-data-maintenance/edit']);
  }

  search() {
    this.pagination.pageNumber === 1 ? this.getData() : this.pagination.pageNumber = 1;
  }

  pageChanged(e: PageChangedEvent) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  uploadFileExcel(event: any) {
    if (event.target.files && event.target.files[0]) {
      const fileNameExtension = event.target.files[0].name.split('.').pop();
      if (!this.extensions.includes(fileNameExtension)) {
        return this.snotifyService.error(MessageConstants.INVALID_FILE, CaptionConstants.ERROR);
      } else {
        const formData = new FormData();
        formData.append('file', event.target.files[0]);
        this.spinnerService.show();
        this.service.upload(formData).subscribe({
          next: (res) => {
            if (res.isSuccess) {
              this.search();
              event.target.value = '';
              this.snotifyService.success(MessageConstants.UPLOADED_OK_MSG ,CaptionConstants.SUCCESS);
            } else {
              this.snotifyService.error( res.error,
                CaptionConstants.ERROR );
            }
            this.inputRef.nativeElement.value = "";
          },
          error: () => {
            event.target.value = '';
            this.snotifyService.error(
              MessageConstants.UPLOAD_ERROR_MSG,
              CaptionConstants.ERROR  );
          }
        }).add(() => this.spinnerService.hide());
      }
    }
  }

  download() {
    this.spinnerService.show();
    this.service.download(this.param).subscribe({
      next: (result: Blob) => {
        this.spinnerService.hide();
        const currentTime = new Date();
        let fileName =
          'Shift_Data_Maintain_' +
          currentTime.getFullYear().toString() +
          (currentTime.getMonth() + 1) +
          currentTime.getDate() +
          currentTime.getHours().toString() + currentTime.getMinutes().toString() + currentTime.getSeconds().toString();

        this.functionUtility.exportExcel(result, fileName);
      },
      error: () => {
        this.snotifyService.error(
          MessageConstants.SYSTEM_ERROR_MSG,
          CaptionConstants.ERROR
        );
        this.spinnerService.hide();
      },
    });
  }
}
