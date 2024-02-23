import { faSearch } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import React from "react";
import { Button, Form } from "react-bootstrap";

const SearchBox = () => {
  return (
    <div className="">
      <Form className="searchbox" method="get">
        <Form.Group className="input-group">
          <Form.Control
            type="text"
            name="k"
            // onSubmit={}
            // ref={keyword}
            placeholder="Tìm kiếm theo từ khoá sản phẩm"
            required
            aria-describedby='btnSearchPost'
          />
          <Button type="submit" id='btnSearchPost'  >
            <FontAwesomeIcon icon={faSearch}></FontAwesomeIcon>
          </Button>
        </Form.Group>
      </Form>
    </div>
  );
};

export default SearchBox;
