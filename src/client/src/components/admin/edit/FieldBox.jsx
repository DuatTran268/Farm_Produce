import React from "react";
import { Form } from "react-bootstrap";


const FieldComment = ({control, notempty}) => {
  return (
    <section>
        <div className="box_comment p-2">
          {control}
          <Form.Control.Feedback type="invalid">
            {notempty}
          </Form.Control.Feedback>
        </div>
    </section>
  );
};
export default FieldComment;



