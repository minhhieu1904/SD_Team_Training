import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Pagination } from '@utilities/pagination-utility';
import {MSLocation} from '../../../../_core/models/mS_Location';
import { MsLocationService } from '../../../../_core/services/ms-location.service'
@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {
  data : MSLocation[] = [];
  
  pagination: Pagination = <Pagination> {
    pageNumber: 1,
    pageSize: 3
  };

  params: MSLocation = <MSLocation> {
    storeH: '',
    locationName: ''
  };

  constructor(private service: MsLocationService, private router: Router) { }

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.service.getData(this.pagination, this.params).subscribe({
      next: res => {
        this.data = res.result;
        this.pagination = res.pagination;
      }
    })
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }

  search() {
    this.pagination.pageNumber = 1;
    this.getData();
  }

  clear() {
    this.params.storeH = "";
    this.params.locationName = "";
    this.getData() == null;
  }

  add() {
    this.router.navigate(['maintain/warehouse-basic-data-maintain/add']);
  }
  
  updateItem(msLocation: MSLocation) {
    console.log(msLocation);
    // đây đúng không 
    // 1. gửi khoá chính qua route để nhận đúng chưa 
    // Gửi trang trang Edit 
    this.router.navigate([`maintain/warehouse-basic-data-maintain/edit/${msLocation.manuf}/${msLocation.storeH}`]); 
  }
}
