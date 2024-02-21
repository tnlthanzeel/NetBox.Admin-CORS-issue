import { AfterViewInit, Component, ElementRef, OnInit, ViewChild, ViewChildren, inject } from '@angular/core';
import { SearchRequestModel } from '../../../core/models/search-request.model';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ServiceTypeService } from '../services/service-type.service';
import { ServiceTypeModel } from '../models/service-type.model';
import { ResponseResult } from '../../../core/models/response-result.model';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { ValidationModel } from '../../shared/validators/validation.model';
import { GenericValidator } from '../../shared/validators/forms-error-validator';
import { Observable, fromEvent, merge } from 'rxjs';
import { ErrorResponse } from '../../../core/models/error-response.model';
import { ToasterService } from '../../../core/services/toaster.service';
import { ModalService } from '../../../core/services/modal.service';
import { ConfirmationModalComponent } from '../../shared/components/confirmation-modal/confirmation-modal.component';

@Component({
  selector: 'netbox-service-manage',
  templateUrl: './service-manage.component.html',
  styleUrl: './service-manage.component.scss'
})
export class ServiceManageComponent implements OnInit, AfterViewInit {

  isBlocked = false;
  pageSizeOptions: number[] = [10, 25, 50, 100];
  searchModel = new SearchRequestModel(10, 1);
  pageIndex = 0;
  displayServiceTypeCreation = false;
  displayServiceCreation = false;
  isFormSubmitted = false;
  isEdit = false;

  serviceTypeForm: FormGroup;
  serviceTypeModel: ServiceTypeModel;

  serviceTypes: ServiceTypeModel[] = [];
  serviceTypeService = inject(ServiceTypeService);
  validationModel: ValidationModel = new ValidationModel();

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  constructor(private formBuilder: FormBuilder, private toasterService: ToasterService,
    private modalService: ModalService) {
    this.validationModel.validationMessages = {
      name: {
        required: 'Service Type is required'
      }
    };
    this.validationModel.formsErrorValidator = new GenericValidator(this.validationModel.validationMessages);
  }

  ngOnInit(): void {
    this.getServiceTypes();
    this.createForm();
  }

  getServiceTypes() {
    this.isBlocked = true;
    this.serviceTypeService.getAll(this.searchModel).subscribe({
      next: (res: ResponseResult<ServiceTypeModel[]>) => {
        this.serviceTypes = res.data;
        this.searchModel.totalRecords = res.totalRecordCount;
        this.isBlocked = false;
      },
    })
  }

  public pageChanged(event: PageEvent): void {
    this.searchModel.pageSize = event.pageSize
    this.searchModel.pageNumber = event.pageIndex + 1;
    this.getServiceTypes();
  }

  ngAfterViewInit(): void {
    this.paginator.pageIndex = this.pageIndex;
    const controlBlurs: Observable<any>[] = this.formInputElements.map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));
    merge(this.serviceTypeForm.valueChanges, ...controlBlurs).subscribe(value => {
      this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.serviceTypeForm, this.isFormSubmitted);
    });
  }

  displayServiceTypeManage() {
    this.displayServiceCreation = false;
    this.displayServiceTypeCreation = !this.displayServiceTypeCreation;
  }

  displayServiceManage(type: ServiceTypeModel) {
    this.serviceTypeModel = type;
    this.displayServiceTypeCreation = false;
    this.isEdit = false;
    this.serviceTypeForm.reset();
    this.displayServiceCreation = true;
  }

  validate(): void {
    this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.serviceTypeForm, this.isFormSubmitted);
  }

  createForm() {
    this.serviceTypeForm = this.formBuilder.group({
      name: ['', Validators.required]
    });
  }

  create() {
    this.isFormSubmitted = true;
    this.validate();
    if (this.serviceTypeForm.invalid) return;

    this.isBlocked = true;
    this.serviceTypeModel = Object.assign({}, this.serviceTypeModel, this.serviceTypeForm.value);
    this.serviceTypeService.create(this.serviceTypeModel).subscribe({
      next: (serviceTypeModel: ResponseResult<ServiceTypeModel>) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.serviceTypeForm.reset();
        this.getServiceTypes();
        this.toasterService.successfullyCreated("Service Category");
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
    if (this.serviceTypeForm.invalid) return;

    this.isBlocked = true;
    this.serviceTypeModel = Object.assign({}, this.serviceTypeModel, this.serviceTypeForm.value);
    this.serviceTypeService.update(this.serviceTypeModel).subscribe({
      next: (serviceTypeModel: object) => {
        this.isBlocked = false;
        this.isEdit = false;
        this.displayServiceTypeCreation = false;
        this.isFormSubmitted = false;
        this.serviceTypeForm.reset();
        this.getServiceTypes();
        this.toasterService.successfullyUpdated("Service Category");
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  getTypeById(type: ServiceTypeModel) {
    this.isBlocked = true
    this.serviceTypeService.getById(type.id).subscribe({
      next: (serviceTypeModel: ResponseResult<ServiceTypeModel>) => {
        this.isEdit = true;
        this.isBlocked = false;
        this.displayServiceCreation = false;
        this.displayServiceTypeCreation = true;
        this.serviceTypeModel = serviceTypeModel.data;
        this.serviceTypeForm.patchValue(this.serviceTypeModel);
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isEdit = false;
        this.displayServiceTypeCreation = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  deleteServiceType(type: ServiceTypeModel) {
    let options = {
      title: 'Delete Service Category',
      message: 'Are you sure you want to delete the service category ?',
    };
    this.modalService.displayDialog(ConfirmationModalComponent, options)
    this.modalService.confirmed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.isBlocked = true;
        this.serviceTypeService.deleteServiceType(type.id).subscribe({
          next: () => {
            this.isBlocked = false;
            this.getServiceTypes();
          },
          error: (err: ErrorResponse) => {
            this.isBlocked = false;
            this.toasterService.error(err);
          }
        })
      }
    });
  }

  isServiceCreated(isCreated: any) {
    if (isCreated)
      this.getServiceTypes();
  }
}
