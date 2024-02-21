import { AfterViewInit, Component, ElementRef, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges, ViewChild, ViewChildren, input } from '@angular/core';
import { FormBuilder, FormControlName, FormGroup, Validators } from '@angular/forms';
import { ModalService } from '../../../../core/services/modal.service';
import { ToasterService } from '../../../../core/services/toaster.service';
import { GenericValidator } from '../../../shared/validators/forms-error-validator';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { ValidationModel } from '../../../shared/validators/validation.model';
import { ErrorResponse } from '../../../../core/models/error-response.model';
import { ResponseResult } from '../../../../core/models/response-result.model';
import { ServiceTypeModel } from '../../models/service-type.model';
import { SearchRequestModel } from '../../../../core/models/search-request.model';
import { Observable, fromEvent, merge } from 'rxjs';
import { ServiceModel } from '../../models/service.model';
import { ServiceItemService } from '../../services/service-item.service';
import { ConfirmationModalComponent } from '../../../shared/components/confirmation-modal/confirmation-modal.component';

@Component({
  selector: 'netbox-service-create',
  templateUrl: './service-create.component.html',
  styleUrl: './service-create.component.scss'
})


export class ServiceCreateComponent implements OnInit, AfterViewInit, OnChanges {

  serviceForm: FormGroup;
  validationModel: ValidationModel = new ValidationModel();
  isBlocked = false;
  pageSizeOptions: number[] = [10, 25, 50, 100];
  searchModel = new SearchRequestModel(10, 1);
  pageIndex = 0;
  displayServiceCreation = false;
  isFormSubmitted = false;
  isEdit = false;

  serviceModel = new ServiceModel();
  services: ServiceModel[] = [];

  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChildren(FormControlName, { read: ElementRef }) formInputElements: ElementRef[];

  @Input() serviceType: ServiceTypeModel;
  @Output() isCreated = new EventEmitter<boolean>();

  constructor(private formBuilder: FormBuilder, private toasterService: ToasterService,
    private modalService: ModalService, private serviceItemService: ServiceItemService) {
    this.validationModel.validationMessages = {
      name: {
        required: 'Service is required'
      },
      rate: {
        required: 'Rate is required'
      }
    };
    this.validationModel.formsErrorValidator = new GenericValidator(this.validationModel.validationMessages);
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.getServices();
  }

  ngOnInit(): void {
    this.getServices();
    this.createForm();
  }

  getServices() {
    this.isBlocked = true;
    this.serviceItemService.getAll(this.serviceType.id, this.searchModel).subscribe({
      next: (res: ResponseResult<ServiceModel[]>) => {
        this.services = res.data;
        this.searchModel.totalRecords = res.totalRecordCount;
        this.isBlocked = false;
      },
    })
  }

  createForm() {
    this.serviceForm = this.formBuilder.group({
      name: ['', Validators.required],
      rate: [0, Validators.required]
    });
  }

  create() {
    this.isFormSubmitted = true;
    this.validate();
    if (this.serviceForm.invalid) return;

    this.isBlocked = true;
    this.serviceModel = Object.assign({}, this.serviceModel, this.serviceForm.value);
    this.serviceModel.serviceTypeId = this.serviceType.id;

    this.serviceItemService.create(this.serviceModel).subscribe({
      next: (ServiceModel: ResponseResult<ServiceModel>) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.serviceForm.reset();
        this.getServices();
        this.isCreated.emit(true);
        this.toasterService.successfullyCreated("Service");
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
    if (this.serviceForm.invalid) return;

    this.isBlocked = true;
    this.serviceModel = Object.assign({}, this.serviceModel, this.serviceForm.value);
    this.serviceModel.serviceTypeId = this.serviceType.id;

    this.serviceItemService.update(this.serviceModel).subscribe({
      next: (serviceModel: object) => {
        this.isBlocked = false;
        this.isEdit = false;
        this.isFormSubmitted = false;
        this.serviceForm.reset();
        this.getServices();
        this.toasterService.successfullyUpdated("Service");
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  public pageChanged(event: PageEvent): void {
    this.searchModel.pageSize = event.pageSize
    this.searchModel.pageNumber = event.pageIndex + 1;
    this.getServices();
  }

  ngAfterViewInit(): void {
    this.paginator.pageIndex = this.pageIndex;
    const controlBlurs: Observable<any>[] = this.formInputElements.map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));
    merge(this.serviceForm.valueChanges, ...controlBlurs).subscribe(value => {
      this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.serviceForm, this.isFormSubmitted);
    });
  }

  validate(): void {
    this.validationModel.displayMessage = this.validationModel.formsErrorValidator.processMessages(this.serviceForm, this.isFormSubmitted);
  }

  getServiceById(service: ServiceModel) {
    this.isBlocked = true
    this.serviceItemService.getById(this.serviceType.id, service.id).subscribe({
      next: (serviceModel: ResponseResult<ServiceModel>) => {
        this.isEdit = true;
        this.isBlocked = false;
        this.serviceModel = serviceModel.data;
        this.serviceForm.patchValue(this.serviceModel);
      },
      error: (err: ErrorResponse) => {
        this.isBlocked = false;
        this.isEdit = false;
        this.isFormSubmitted = false;
        this.toasterService.error(err);
      }
    })
  }

  deleteService(service: ServiceModel) {
    let options = {
      title: 'Delete Service',
      message: 'Are you sure you want to delete the service?',
    };
    this.modalService.displayDialog(ConfirmationModalComponent, options)
    this.modalService.confirmed().subscribe((confirmed: boolean) => {
      if (confirmed) {
        this.isBlocked = true;
        this.serviceItemService.deleteServiceType(this.serviceType.id, service.id).subscribe({
          next: () => {
            this.isBlocked = false;
            this.getServices();
          },
          error: (err: ErrorResponse) => {
            this.isBlocked = false;
            this.toasterService.error(err);
          }
        })
      }
    });
  }

}
