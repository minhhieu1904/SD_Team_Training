import { CaptionConstants, MessageConstants } from '@constants/message.enum';
import { IconButton } from '@constants/common.constants';
import { BsDatepickerConfig } from 'ngx-bootstrap/datepicker';
import { Pagination } from '@utilities/pagination-utility';
import { ReportSortSumService } from '@services/Report/Report-Sort-Sum.service';
import { InjectBase } from '@utilities/inject-base-app';
import { Component, OnInit } from '@angular/core';
import {
  Brand,
  Report_Sort_SumParam,
  SortSumDTO,
  SortSumDeltailDTOParam,
} from '@models/report/sort-Sum';
import { KeyValuePair } from '@utilities/key-value-pair';
import { LangConstants } from '@constants/lang-constant';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent extends InjectBase implements OnInit {
  dateKind: KeyValuePair[] = [
    { key: 'sdat', value: 'sdat_Date' },
    { key: 'mdat', value: 'Production_Date' },
  ];
  ischeckItem: boolean = false;
  dataChecked: Report_Sort_SumParam;
  constructor(private service: ReportSortSumService) {
    super();

  }
  brand: Brand[] = [];

  exprotdetail :SortSumDeltailDTOParam = <SortSumDeltailDTOParam>{
    manno: '',
    purno: '',
    size: '',
  };
  lang: string = localStorage.getItem(LangConstants.LANG) != null ? localStorage.getItem(LangConstants.LANG):'en'

  paramsExport: Report_Sort_SumParam = <Report_Sort_SumParam>{};
  data: Report_Sort_SumParam[] = [];
  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 10,
  };
  param: SortSumDTO = <SortSumDTO>{
    crDay: '',
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
    eta_start: '',
    eta_end: '',
    size: '',
  };
  bsConfig?: Partial<BsDatepickerConfig>;
  dateFrom_mdat = '';
  dateTo_mdat = '';
  dateFrom_eta = '';
  dateTo_eta = '';
  crDay= '';
  iconButton = IconButton;

  ngOnInit(): void {
    this.search();
    this.getBrand();

  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  getBrand() {
    this.spinnerService.show();
    this.service.getBrand().subscribe({
      next: (res) => {
        this.brand = res;
        this.spinnerService.hide();
      },
      error: () => this.spinnerService.hide(),
    });
  }

  getData() {
    this.param.date_start = !this.functionUtility.checkEmpty(this.dateFrom_mdat)
      ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat))
      : '';
    this.param.date_end = !this.functionUtility.checkEmpty(this.dateTo_mdat)
      ? this.functionUtility.getDateFormat(new Date(this.dateTo_mdat))
      : '';
    this.param.eta_start = !this.functionUtility.checkEmpty(this.dateFrom_eta)
      ? this.functionUtility.getDateFormat(new Date(this.dateFrom_eta))
      : '';
    this.param.eta_end = !this.functionUtility.checkEmpty(this.dateTo_eta)
      ? this.functionUtility.getDateFormat(new Date(this.dateTo_eta))
      : '';
    this.spinnerService.show();

    this.service.getData(this.pagination, this.param).subscribe({
      next: (res) => {
        res.result;
        res.result.map((ngay) =>
          this.functionUtility.getDateFormat(new Date(ngay.mdat))
        );
        this.data = res.result;
        this.pagination = res.pagination;
        this.spinnerService.hide();
      },
      error: () => {},
    });
  }

  search() {
    this.pagination.pageNumber === 1
      ? this.getData()
      : (this.pagination.pageNumber = 1);
  }

  clear() {
      this.param.crDay = '',
      this.param.date_start = '',
      this.param.date_kind = '',
      this.param.date_end = '',
      this.param.brandname = '',
      this.param.cusna = '',
      this.param.manno = '',
      this.param.rmodel = '',
      this.param.tolcls = '',
      this.param.purno = '',
      this.param.bitnbr = '',
      this.param.kind = '',
      this.param.eta_start = '',
      this.param.eta_end = '',
      this.param.size = '',
      this.dateFrom_mdat = '';
      this.dateTo_mdat = '';
      this.dateFrom_eta = '';
      this.dateTo_eta = '';
      this.getData();
  }

  export() {
    this.param.date_start = !this.functionUtility.checkEmpty(this.dateFrom_mdat)
      ? this.functionUtility.getDateFormat(new Date(this.dateFrom_mdat))
      : '';
    this.param.date_end = !this.functionUtility.checkEmpty(this.dateTo_mdat)
      ? this.functionUtility.getDateFormat(new Date(this.dateTo_mdat))
      : '';
    this.param.eta_start = !this.functionUtility.checkEmpty(this.dateFrom_eta)
      ? this.functionUtility.getDateFormat(new Date(this.dateFrom_eta))
      : '';
    this.param.eta_end = !this.functionUtility.checkEmpty(this.dateTo_eta)
      ? this.functionUtility.getDateFormat(new Date(this.dateTo_eta))
      : '';
    this.spinnerService.show();
    this.service
      .exportExcel(this.paramsExport)
      .subscribe({
        next: (result: Blob) => {
          this.functionUtility.exportExcel(result, 'Report Sort Sum');
        },
        error: () => {
          this.spinnerService.hide();
          this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error')
         );
        },
      })
      .add(() => this.spinnerService.hide());
  }


  exportDetails() {

      this.crDay = !this.functionUtility.checkEmpty(this.crDay)
      ? this.functionUtility.getDateFormat(new Date(this.crDay))
      : '';

    this.spinnerService.show();
    this.service
      .exportExcelDetail(this.exprotdetail)
      .subscribe({
        next: (result: Blob) => {
          this.functionUtility.exportExcel(result, 'Report Sort Sum Detail');
        },
        error: () => {
          this.spinnerService.hide();
          this.snotifyService.error(this.translateService.instant('System.Message.UnknowError'), this.translateService.instant('System.Caption.Error'))

        },
      })
  }

checkRecord(item: Report_Sort_SumParam) {
    item.ischeck = !item.ischeck;
    this.ischeckItem = item.ischeck;
    this.data.filter(x => x != item).forEach(x => { x.ischeck = false });
    this.dataChecked = item;

    this.exprotdetail.manno = item.manno;
    this.exprotdetail.purno = item.purno;
    this.exprotdetail.size = item.size;
  }
}
