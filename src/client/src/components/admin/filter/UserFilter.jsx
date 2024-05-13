import React from "react";

import { Form } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import { reset, updateName } from "../../../redux/UserRedux";

const UserFilter = () => {
  const userFilter = useSelector((state) => state.userFilter),
    dispatch = useDispatch();

  const handleReset = (e) => {
    dispatch(reset());
  };

  return (
    <Form method="get" onReset={handleReset} className="col-2">
      <Form.Group className="col-auto">
        <Form.Label className="visually-hidden">Tên</Form.Label>
        <Form.Control
          type="text"
          placeholder="Tìm kiếm theo tên"
          name="name"
          value={userFilter.name}
          onChange={(e) => dispatch(updateName(e.target.value))}
        />
      </Form.Group>
    </Form>
  );
};
export default UserFilter;
