import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateTableDialogComponent } from './create-table-dialog.component';

describe('CreateTableDialogComponent', () => {
  let component: CreateTableDialogComponent;
  let fixture: ComponentFixture<CreateTableDialogComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateTableDialogComponent]
    });
    fixture = TestBed.createComponent(CreateTableDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
