import { Component, ElementRef, OnInit, ViewChild, ViewChildren, inject } from '@angular/core';
import { FormControlName, FormBuilder, Validators } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Observable, fromEvent, merge } from 'rxjs';
import { ErrorResponse } from '../../../core/models/error-response.model';
import { ResponseResult } from '../../../core/models/response-result.model';
import { SearchRequestModel } from '../../../core/models/search-request.model';
import { ModalService } from '../../../core/services/modal.service';
import { ToasterService } from '../../../core/services/toaster.service';
import { ConfirmationModalComponent } from '../../shared/components/confirmation-modal/confirmation-modal.component';
import { ValidationModel } from '../../shared/validators/validation.model';
import { AdvertisementMaterialService } from '../services/advertisement-material.service';
import { AdvertisementMaterialModel } from '../models/advertisement-material.model';
import { environment } from '../../../../environments/environment';
import { AppPermissions } from '../../../core/extenstion/permissions';

@Component({
  selector: 'netbox-advertisement-material',
  templateUrl: './advertisement-material.component.html',
  styleUrl: './advertisement-material.component.scss'
})
export class AdvertisementMaterialComponent implements OnInit {

  isBlocked = false;
  pageSizeOptions: number[] = [10, 25, 50, 100];
  searchModel = new SearchRequestModel(10, 1);
  pageIndex = 0;
  displayadMatrialCreation = false;
  isFormSubmitted = false;
  isEdit = false;

  selectedFile: File | null;
  selectedImageUrl: string | null | ArrayBuffer;
  selectedVideoUrl: string | null | ArrayBuffer;
  fileName = '';

  adMaterialModel = new AdvertisementMaterialModel();
  adMaterials: AdvertisementMaterialModel[] = [];
  adMaterialService = inject(AdvertisementMaterialService);
  validationModel: ValidationModel = new ValidationModel();

  p = AppPermissions;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  constructor(private toasterService: ToasterService,
    private modalService: ModalService) {
  }

  ngOnInit(): void {
    this.getAdvertismentMaterials();
  }

  getAdvertismentMaterials() {
    this.isBlocked = true;
    this.adMaterialService.getAll(this.searchModel).subscribe({
      next: (res: ResponseResult<AdvertisementMaterialModel[]>) => {
        this.adMaterials = res.data;
        this.searchModel.totalRecords = res.totalRecordCount;
        this.isBlocked = false;
      },
    })
  }

  public pageChanged(event: PageEvent): void {
    this.searchModel.pageSize = event.pageSize
    this.searchModel.pageNumber = event.pageIndex + 1;
    this.getAdvertismentMaterials();
  }

  displayAdMaterialManage() {
    this.isEdit = false;
    this.clear();
    this.displayadMatrialCreation = true;
  }


  create() {
    this.isBlocked = true;
    this.adMaterialModel.file = this.selectedFile!;

    this.adMaterialService.create(this.adMaterialModel).subscribe({
      next: (res: ResponseResult<AdvertisementMaterialModel>) => {
        this.isBlocked = false;
        this.clear();
        this.getAdvertismentMaterials();
        this.toasterService.successfullyCreated("Advertising Material");
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  clear() {
    this.selectedFile = null;
    this.selectedImageUrl = '';
    this.selectedVideoUrl = '';
    this.fileName = '';
  }


  deleteAdMaterial(adMaterialModel: AdvertisementMaterialModel) {
    let options = {
      title: 'Delete Advertising Material',
      message: 'Are you sure you want to delete the Advertising Material ?',
    };
    this.modalService.displayDialog(ConfirmationModalComponent, options)
    this.modalService.confirmed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.isBlocked = true;
        this.adMaterialService.deleteAdMaterial(adMaterialModel.id).subscribe({
          next: () => {
            this.isBlocked = false;
            this.getAdvertismentMaterials();
          },
          error: (err: ErrorResponse) => {
            this.isBlocked = false;
            this.toasterService.error(err);
          }
        })
      }
    });
  }

  onFileSelected(event: any): void {
    this.selectedFile = event.target.files[0];

    console.log(this.selectedFile);

    if (this.selectedFile) {
      const mimeType = this.selectedFile.type;
      const reader = new FileReader();

      if ((mimeType.match(/image\/*/) == null) && (mimeType.match(/video\/*/) == null)) {
        this.toasterService.warning("Only images and videos are supported. ");
        return;
      }

      if (mimeType.match(/image\/*/) != null) {
        reader.readAsDataURL(this.selectedFile);
        reader.onload = (_event) => {
          this.selectedImageUrl = reader.result;
        }
      }

      if (mimeType.match(/video\/*/) != null) {
        reader.readAsDataURL(this.selectedFile);
        reader.onload = (_event) => {
          this.selectedVideoUrl = reader.result;
        }
      }

      this.fileName = this.selectedFile.name;
    }
  }
}
