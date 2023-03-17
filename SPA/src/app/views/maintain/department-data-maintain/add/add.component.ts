import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { MSDepartment } from '@models/mS_Department';
import { MSDepartmentService } from '@services/ms-department.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  msDepartment: MSDepartment = <MSDepartment>{
    manuf: "",
    parNo: "",
    parName: ""
  }
  constructor(
    private router: Router,
    private service: MSDepartmentService
    ) { }

  ngOnInit(): void {
  }


  backList() {
    this.router.navigate(['maintain/department-data-maintain'])
  }

  save() {
    this.service.addNew(this.msDepartment).subscribe({
      next: res => {
        if(res.isSuccess) {
          alert('Them thanh cong')
        }
      },
      error: () => {
        alert('Them that bai')
      }      
    })
  }

  saveAndNext() {
    this.service.addNew(this.msDepartment).subscribe({
      next: res => {
        if(res.isSuccess) {
          alert('Them thanh cong')
        }
      },
      error: () => {
        alert('Them that bai')
      },
      complete: () => {
        this.backList();
      }
    })
  }
}
