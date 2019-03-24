import { Component, OnInit } from '@angular/core';
import { UploadImageService } from '../shared/upload-image.service';

@Component({
  selector: 'app-upload-image',
  templateUrl: './upload-image.component.html',
  styleUrls: ['./upload-image.component.css'],
  providers: [UploadImageService]
})
export class UploadImageComponent implements OnInit {
  imageUrl = '/assets/img/default-image.png';
  fileToUpload: File = null;
  constructor(private imageService: UploadImageService) { }

  ngOnInit() {
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);

    // Show image preview
    const reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result;
    };
    reader.readAsDataURL(this.fileToUpload);
  }

  OnSubmit(Caption, Image, Caption2) {
   this.imageService.postFile(Caption.value, this.fileToUpload, Caption2.value,).subscribe(
     data => {
       console.log('done');
       Caption.value = null;
       Image.value = null;
       this.imageUrl = '/assets/img/default-image.png';
     }
   );
  }

}
