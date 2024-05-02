import React from "react";
import { Form } from "react-bootstrap";

const BoxEdit = ({label, control, notempty}) => {
  return (
    <section>
      <div className="row mb-3">
        <Form.Label className="col-sm-2 col-form-label">
          {label}
        </Form.Label>
        <div className="col-sm-10 form_input">
          {control}
          <Form.Control.Feedback type="invalid">
            {notempty}
          </Form.Control.Feedback>
        </div>
      </div>
    </section>
  );
};
export default BoxEdit;



