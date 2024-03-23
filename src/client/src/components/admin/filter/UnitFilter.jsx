import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { reset, updateName } from "../../../redux/Unit";
import { Form } from "react-bootstrap";

const UnitFilter = () => {
  const unitFilter = useSelector((state) => state.unitFilter),
    dispatch = useDispatch();

  const handleReset = (e) => {
    dispatch(reset());
  };

  return (
    <Form
      method="get"
      onReset={handleReset}
      className="col-2"
    >
      <Form.Group className="col-auto">
        <Form.Label className="visually-hidden">Tên</Form.Label>
        <Form.Control
          type="text"
          placeholder="Tìm kiếm theo tên"
          name="name"
          value={unitFilter.name}
          onChange={(e) => dispatch(updateName(e.target.value))}
        />
      </Form.Group>
    </Form>
  );
};
export default UnitFilter;
