import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, ViewChildren, inject } from '@angular/core';
import { FormGroup, FormControlName, FormBuilder, Validators } from '@angular/forms';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { Observable, fromEvent, merge } from 'rxjs';
import { ErrorResponse } from '../../../core/models/error-response.model';
import { ResponseResult } from '../../../core/models/response-result.model';
import { SearchRequestModel } from '../../../core/models/search-request.model';
import { ModalService } from '../../../core/services/modal.service';
import { ToasterService } from '../../../core/services/toaster.service';
import { ConfirmationModalComponent } from '../../shared/components/confirmation-modal/confirmation-modal.component';
import { GenericValidator } from '../../shared/validators/forms-error-validator';
import { ValidationModel } from '../../shared/validators/validation.model';
import { DesignReceivedMediumModel } from '../models/design-received-medium.model';
import { DesignReceivedMediumService } from '../services/design-received-medium.service';
import { AppPermissions } from '../../../core/extenstion/permissions';

@Component({
  selector: 'netbox-design-received-medium',
  templateUrl: './design-received-medium.component.html',
  styleUrl: './design-received-medium.component.scss'
})
export class DesignReceivedMediumComponent implements OnInit, AfterViewInit {

  isBlocked = false;
  pageSizeOptions: number[] = [10, 25, 50, 100];
  searchModel = new SearchRequestModel(10, 1);
  pageIndex = 0;
  displayDesignMediumCreation = false;
  isFormSubmitted = false;
  isEdit = false;

  designMediumForm: FormGroup;
  designMediumModel: DesignReceivedMediumModel;
  selectedFile: File | null;
  selectedImageUrl: string | null | ArrayBuffer;
  fileName = '';

  designMediums: DesignReceivedMediumModel[] = [];
  designMediumService = inject(DesignReceivedMediumService);
  validationModel: ValidationModel = new ValidationModel();

  p = AppPermissions;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  constructor(private formBuilder: FormBuilder, private toasterService: ToasterService,
    private modalService: ModalService) {
    this.validationModel.validationMessages = {
      mode: {
        required: 'Medium is required'
      }
    };
    this.validationModel.formsErrorValidator = new GenericValidator(this.validationModel.validationMessages);
  }

  ngOnInit(): void {
    this.getDesignMediums();
    this.createForm();
  }

  getDesignMediums() {
    this.isBlocked = true;
    this.designMediumService.getAll(this.searchModel).subscribe({
      next: (res: ResponseResult<DesignReceivedMediumModel[]>) => {
        this.designMediums = res.data;
        this.searchModel.totalRecords = res.totalRecordCount;
        this.isBlocked = false;
      },
    })
  }

  public pageChanged(event: PageEvent): void {
    this.searchModel.pageSize = event.pageSize
    this.searchModel.pageNumber = event.pageIndex + 1;
    this.getDesignMediums();
  }

  ngAfterViewInit(): void {
    this.paginator.pageIndex = this.pageIndex;
    const controlBlurs: Observable<any>[] = this.formInputElements.map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));
    merge(this.designMediumForm.valueChanges, ...controlBlurs).subscribe(value => {
      this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.designMediumForm, this.isFormSubmitted);
    });
  }

  displayJobTypeManage() {
    this.isEdit = false;
    this.clear();
    this.displayDesignMediumCreation = true;
  }

  clear() {
    this.designMediumForm.reset();
    this.selectedFile = null;
    this.selectedImageUrl = null;
    this.fileName = '';
  }


  validate(): void {
    this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.designMediumForm, this.isFormSubmitted);
  }

  createForm() {
    this.designMediumForm = this.formBuilder.group({
      mode: ['', Validators.required],
      image: [File]
    });
  }

  create() {
    this.isFormSubmitted = true;
    this.validate();
    if (this.designMediumForm.invalid) return;

    this.isBlocked = true;
    this.designMediumModel = Object.assign({}, this.designMediumModel, this.designMediumForm.value);
    this.designMediumModel.image = this.selectedFile!;
    this.designMediumService.create(this.designMediumModel).subscribe({
      next: (res: ResponseResult<DesignReceivedMediumModel>) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.clear();
        this.getDesignMediums();
        this.toasterService.successfullyCreated("Design Sent Mode");
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  update() {
    this.isFormSubmitted = true;
    this.validate();
    if (this.designMediumForm.invalid) return;

    this.isBlocked = true;
    this.designMediumModel = Object.assign({}, this.designMediumModel, this.designMediumForm.value);
    this.designMediumModel.image = this.selectedFile!;
    this.designMediumService.update(this.designMediumModel).subscribe({
      next: (res: object) => {
        this.isBlocked = false;
        this.isEdit = false;
        this.displayDesignMediumCreation = false;
        this.isFormSubmitted = false;
        this.clear();
        this.getDesignMediums();
        this.toasterService.successfullyUpdated("Design Sent Mode");
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  getDesignMediumById(designMedium: DesignReceivedMediumModel) {
    this.isBlocked = true
    this.designMediumService.getById(designMedium.id).subscribe({
      next: (designMedium: ResponseResult<DesignReceivedMediumModel>) => {
        this.isEdit = true;
        this.isBlocked = false;
        this.displayDesignMediumCreation = true;
        this.designMediumModel = designMedium.data;
        this.selectedImageUrl = this.designMediumModel.imageURL;
        this.designMediumForm.patchValue(this.designMediumModel);
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isEdit = false;
        this.displayDesignMediumCreation = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  deleteDesignMedium(designMedium: DesignReceivedMediumModel) {
    let options = {
      title: 'Delete design sent mode',
      message: 'Are you sure you want to delete the design sent mode ?',
    };
    this.modalService.displayDialog(ConfirmationModalComponent, options)
    this.modalService.confirmed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.isBlocked = true;
        this.designMediumService.deleteDesignMedium(designMedium.id).subscribe({
          next: () => {
            this.isBlocked = false;
            this.getDesignMediums();
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

    if (this.selectedFile) {
      const mimeType = this.selectedFile.type;
      if (mimeType.match(/image\/*/) == null) {
        this.toasterService.warning("Only images are supported.");
        return;
      }

      const reader = new FileReader();
      reader.readAsDataURL(this.selectedFile);
      reader.onload = (_event) => {
        this.selectedImageUrl = reader.result;
      }

      this.fileName = this.selectedFile.name;
    }
  }
}


