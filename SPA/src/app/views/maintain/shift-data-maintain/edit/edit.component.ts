import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MSShift } from '@models/common/ms-shift';
import { MsShiftService } from '@services/main/ms-shift.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.scss']
})
export class EditComponent implements OnInit {
  // khai báo 1 biến hứng dữ liệu trả về để hiển thị ở giao diện
  msShiftUpdate: MSShift = <MSShift>{
    manuf: '',
    shift: '',
    shiftName: ''
  };

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    // Khởi tạo service vào constructor để sử dụng
    private service: MsShiftService
  ) { }

  ngOnInit() {
    // Tại OnInit lấy các data cần lấy về
    let manuf = '';
    let shift = '';
    this.route.params.subscribe(params => {
      manuf = params['manuf']; // id: này được lấy từ đường dẫn cài đặt
      shift = params['shift'];
      // Chạy hàm đêt lấy dữ liệu khi được chuyển trang qua đây
      this.getMsShift(manuf, shift);
    });
    console.log('dữ liệu lấy tại đường dẫn:', manuf, shift);
    this.updateShift();
  }

  // Anh sẽ viết 1 hàm để lấy dữ liệu theo 2 khóa chính  về : Trả về 1 item có mã khóa chính đã lấy

  /// Truyền vào khóa chính Manuf và khóa chính Shift ,
  getMsShift(manuf: string, shift: string) {
    // gọi lên server để lấy dữ liệu, chưa có hàm get item
    this.service.getItem(manuf, shift).subscribe({
      next: msShift => {
        // gán msShiftUpdate = data từ API trả về
        this.msShiftUpdate = msShift;
        console.log('data cần cập nhật:', this.msShiftUpdate);

      },
      error: (err) => console.log(err), // có 1 dòng thì viết ngắn gon như này cũng được
      complete: () => console.log('complete')
    })
  }

  updateShift() {
    this.service.updateShift(this.msShiftUpdate).subscribe({
      next: result => {
        if (result.isSuccess)
          alert('Update thanh cong')
      }, error: error => {
        alert('Khong them duoc')
      }, complete: () => {
      }
    })
  }

  backList() {
    this.router.navigate(['maintain/shift-data-maintain']);
  }
}
