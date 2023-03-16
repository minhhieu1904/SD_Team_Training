import { routes } from './../../../../app.routing';
import { LocationDataMaintenanceService } from './../../../../_core/services/warehouse-basic-data-maintenance.service';
import { Router } from '@angular/router';
import { IconButton } from './../../../../_core/constants/common.constants';
import { Pagination } from './../../../../_core/utilities/pagination-utility';
import { MS_Location } from './../../../../_core/models/mS_Location_DTO';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
})
export class MainComponent implements OnInit {
  param: MS_Location = {} as MS_Location;

  data: MS_Location[] = [];
  pagination: Pagination = {
    pageNumber: 1,
    pageSize: 5,
  } as Pagination;
  iconButton = IconButton;

  constructor(
    private service: LocationDataMaintenanceService,
    private route: Router
  ) {}

  ngOnInit(): void {
    this.getData();
  }

  getData() {
    this.service.getData(this.param, this.pagination).subscribe({
      next: (res) => {
        console.log(res);
        if (res) {
          this.data = res.result;
          this.pagination = res.pagination;
        }
      },
      error: () => {},
      complete: () => {},
    });
  }

  search() {
    this.pagination.pageNumber === 1
      ? this.getData()
      : (this.pagination.pageNumber = 1);
  }

  clear() {
    this.param = {} as MS_Location;
    this.getData();
  }

  addNew() {
    this.route.navigate(['maintain/location-data-maintenance/add']);
  }

  update() {
    this.route.navigate(['maintain/location-data-maintenance/update']);
  }

  pageChanged(e: any) {
    this.pagination.pageNumber = e.page;
    this.getData();
  }
}
