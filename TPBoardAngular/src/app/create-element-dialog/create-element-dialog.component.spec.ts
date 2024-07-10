import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateElementDialogComponent } from './create-element-dialog.component';

describe('CreateElementDialogComponent', () => {
  let component: CreateElementDialogComponent;
  let fixture: ComponentFixture<CreateElementDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateElementDialogComponent]
    });
    fixture = TestBed.createComponent(CreateElementDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
