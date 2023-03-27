import { Component, OnInit } from '@angular/core';
import { MSPackage } from '@models/common/mS_Package';
import { MSPackageService } from '@services/main/ms-package.service';
import { Pagination } from '@utilities/pagination-utility';
import { Router } from '@angular/router';
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  data: MSPackage[] = [];

  pagination: Pagination = <Pagination>{
    pageNumber: 1,
    pageSize: 5
  }

  params: MSPackage = <MSPackage>{
    packageNo: '',
    packageQty: 0
  }

  constructor(
    private services: MSPackageService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.services.getData(this.pagination, this.params).subscribe({
      next: res => {
        this.data = res.result;
        this.pagination = res.pagination;
      }, error: (err) => { console.log(err) },
      complete: () => {
        console.log('complete');
      }
    })
  }

  add() {
    this.router.navigate(['maintain/package-data-maintain/add']);
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getData();
  }

  clear() {
    this.params.packageNo = '';
    this.params.packageQty = 0;
    this.getData() == null;
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  updateItem(msPackage: MSPackage) {
    this.router.navigate([`maintain/package-data-maintain/edit/${msPackage.manuf}/${msPackage.packageNo}`]);
  }
}
