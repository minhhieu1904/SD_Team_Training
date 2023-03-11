import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import { MSShift } from '@models/ms-shift';
import { MsShiftService } from '@services/ms-shift.service';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss']
})
export class AddComponent implements OnInit {
  msshift: MSShift = <MSShift>{
    manuf: '',
    shift: '',
    shiftName: ''
  };

  // msShift2 : MSShift = {
  //   manuf: '',
  //   shift: '',
  //   shiftName: ''
  // } as MSShift;

  constructor(
    private router: Router,
    private msShiftService: MsShiftService
  ) { }

  ngOnInit() {
    
  }

  backList() {
    this.router.navigate(['maintain/shift-data-maintain']);
  }

  save() {
    console.log(this.msshift);
    this.msShiftService.addShift(this.msshift).subscribe({
      // sau khi khi thành công thì làm gì tiếp theo
      next: result => {
        if(result.isSuccess)
        alert('Them thanh cong');
      },
      // Nếu bị lỗi , thì làm gì ở đây
      error: error => {
        alert('Khong them duoc');
      },
      // sau khi hoàn tất thì làm gì 
      complete: () => {
         this.msshift.shift = "";
         this.msshift.shiftName = "";
      }
    });
  }

  saveAndNext() {
    this.msShiftService.addShift(this.msshift).subscribe({
      next: result => {
        if(result.isSuccess)
        this.backList();
      },
      error: () => {
        alert('Khong them duoc');
           },
      complete: () => {        
      }
    })
  }
}
