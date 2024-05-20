import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { reset, updateName} from "../../../redux/ProductRedux";
import { Form } from "react-bootstrap";
import "./FilterBox.css"


const ProductFilter = () => {
  const productFilter = useSelector((state) => state.productFilter),
    dispatch = useDispatch();

  const handleReset = (e) => {
    dispatch(reset());
  };

  return (
    <Form method="get" onReset={handleReset} className="col-2">
      <Form.Group className="col-auto box_search">
        <Form.Label className="visually-hidden">Tên</Form.Label>
        <Form.Control
        className="filter_box"
          type="text"
          placeholder="Tìm kiếm theo tên"
          name="name"
          value={productFilter.name}
          onChange={(e) => dispatch(updateName(e.target.value))}
        />
      </Form.Group>
    </Form>
  );
};
export default ProductFilter;
