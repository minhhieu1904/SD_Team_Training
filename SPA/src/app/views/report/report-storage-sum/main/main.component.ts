import { Component, OnInit } from '@angular/core';
import { IconButton } from '@constants/common.constants';
import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { MSQRStorageDto, ReportStorageSum, ReportStorageSumDetailParam } from '@models/report/report_Storage_Sum';
import { Brand } from '@models/report/report_wksh_Sum';
import { NgSnotifyService } from '@services/common/ng-snotify.service';
import { ReportStorageSumService } from '@services/report/report-storage-sum.service';
import { FunctionUtility } from '@utilities/function-utility';
import { KeyValueUtility } from '@utilities/key-value-utility';
import { Pagination } from '@utilities/pagination-utility';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  actives: KeyValueUtility[] = [
    { key: 'sdat', value: 'Sdat Date' },
    { key: 'mdat', value: 'Production_Date' },
  ]
  dataCheck: any;
  brand: Brand[] = [];
  isDisableDetailExcel: boolean = false;
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10
  };
  data: MSQRStorageDto[] = [];
  data1: MSQRStorageDto = <MSQRStorageDto>{};
  param1: ReportStorageSum = <ReportStorageSum>{};
  paramDetail: ReportStorageSumDetailParam = <ReportStorageSumDetailParam>{
    manno: '',
    purno: '',
    size: ''
  };
  param: ReportStorageSum = <ReportStorageSum>{
    date_kind: '',
    date_start: '',
    date_end: '',
    brandname: '',
    cusna: '',
    manno: '',
    purno: '',
    rmodel: '',
    tolcls: '',
    bitnbr: '',
    kind: '',
    etd_start: '',
    etd_end: '',
    size: ''
  };
  bsConfig?: Partial<BsDatepickerConfig>;
  iconButton = IconButton;
  dateKind = "";
  dateFrom_date = "";
  dateTo_date = "";
  dateTo_etd = "";
  dateFrom_etd = "";

  selectedIndex: number;

  constructor(
    private service: ReportStorageSumService,
    private spinnerService: NgxSpinnerService,
    private snotifyService: NgSnotifyService,
    private functionUtility: FunctionUtility,
  ) { }

  ngOnInit(): void {
    this.getData();
    this.getBrand();
  }

  checkboxChange(item: MSQRStorageDto) {
    item.check = !item.check
    this.isDisableDetailExcel = item.check;
    this.data.filter(x => x != item).forEach(x => { x.check = false });
    this.dataCheck = item;
  }

  getBrand() {
    this.service.getBrand().subscribe({
      next: res => {
        this.brand = res;
      },
      error: err => {
        console.log(err);
      }
    })
  }

  exportExcelDetail() {
    this.paramDetail.manno = this.dataCheck.manno;
    this.paramDetail.purno = this.dataCheck.purno;
    this.paramDetail.size = this.dataCheck.size;
    console.log(this.paramDetail);
    this.spinnerService.show();
    this.service.getExcelDetail(this.paramDetail).subscribe({
      next: (res: Blob) => {
        if (res != null) {
          this.functionUtility.exportExcel(res, 'StorageDetailReport');
          this.snotifyService.success("EXPORT SUCCESS", CaptionConstants.SUCCESS);
        } else
          this.snotifyService.warning(MessageConstants.NO_DATA, CaptionConstants.WARNING);
      }, error: () => {
        this.snotifyService.error("EXPORT ERROR", CaptionConstants.ERROR);
      },
      complete: () => {
        this.spinnerService.hide();
      }
    })
  }

  exportExcel() {
    this.spinnerService.show();
    this.service.getExcel(this.param).subscribe({
      next: (res: Blob) => {
        this.functionUtility.exportExcel(res, 'ReportStorageSum');
        this.snotifyService.success("EXPORT SUCCESS", CaptionConstants.SUCCESS);
      }, error: () => {
        this.snotifyService.error("EXPORT ERROR", CaptionConstants.ERROR);
      },
      complete: () => {
        this.spinnerService.hide();
      }
    })
  }

  clear() {
    this.param.date_kind = '',
      this.param.date_start = '',
      this.param.date_end = '',
      this.param.brandname = '',
      this.param.cusna = '',
      this.param.manno = '',
      this.param.rmodel = '',
      this.param.tolcls = '',
      this.param.purno = '',
      this.param.bitnbr = '',
      this.param.kind = '',
      this.param.etd_start = '',
      this.param.etd_end = '',
      this.param.size = ''
    this.search();
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getData();
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  getData() {
    this.param.date_kind = !this.functionUtility.checkEmpty(this.dateKind) ? this.functionUtility.getDateFormat(new Date(this.dateKind)) : "";
    this.param.date_start = !this.functionUtility.checkEmpty(this.dateTo_date) ? this.functionUtility.getDateFormat(new Date(this.dateTo_date)) : "";
    this.param.date_end = !this.functionUtility.checkEmpty(this.dateFrom_date) ? this.functionUtility.getDateFormat(new Date(this.dateFrom_date)) : "";
    this.param.etd_start = !this.functionUtility.checkEmpty(this.dateTo_etd) ? this.functionUtility.getDateFormat(new Date(this.dateTo_etd)) : "";
    this.param.etd_end = !this.functionUtility.checkEmpty(this.dateFrom_etd) ? this.functionUtility.getDateFormat(new Date(this.dateFrom_etd)) : "";

    this.spinnerService.show();
    this.service.getData(this.pagination, this.param).subscribe({
      next: res => {
        console.log(res);
        this.data = res.result;
        this.pagination = res.pagination;
        this.spinnerService.hide();
      },
      error: () => {
        this.snotifyService.error(MessageConstants.UPDATED_ERROR_MSG, CaptionConstants.ERROR)
      },
      complete: () => {
        console.log("complete !!! ㄟ( ▔, ▔ )ㄏ");
      }
    })
  }
}
