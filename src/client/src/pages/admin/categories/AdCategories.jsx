import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import HeaderBtn from "../../../components/common/HeaderBtn";
import BtnSuccess from "../../../components/common/BtnSuccess";
import UnitFilter from "../../../components/admin/filter/UnitFilter";
import { faAdd, faEdit, faTrash } from "@fortawesome/free-solid-svg-icons";
import Loading from "../../../components/common/Loading";
import { Table } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import BtnNextPage from "../../../components/common/BtnNextPage";
import { Link, useParams } from "react-router-dom";
import { useSnackbar } from "notistack";
import { deletCategory, getFilterCategory } from "../../../api/Category";
import { useSelector } from "react-redux";
import Popup from "../../../components/common/Popup";

const AdCategory = () => {

  const [getCategory, setgetCategory] = useState([]);
  const [reRender, setRender] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [metadata, setMetadata] = useState({});
  const [pageNumber, setPageNumber] = useState(1);

  const [isVisibleLoading, setIsVisibleLoading] = useState(true),
    unitFilter = useSelector((state) => state.unitFilter);
  
  const [popupMessage, setPopupMessage] = useState("");
  const [popupVisible, setPopupVisible] = useState(false);
  const [IdToDelete, setIdToDelete] = useState(null);

  let { id } = useParams,
    p = 1,
    ps = 5;

  function updatePageNumber(inc) {
    setPageNumber((currentVal) => currentVal + inc);
  }

  useEffect(() => {
    document.title = "Quản lý Danh mục sản phẩm";

    loadDepartment();
    async function loadDepartment() {
      function setData(props) {
        setgetCategory(props.items);
        setMetadata(props.metadata);
      }
      getFilterCategory(unitFilter.name, ps, pageNumber).then((data) => {
        if (data) {
          setData(data);
          // console.log("Check data catgego",data)
        } else {
          setData([]);
        }
        setIsVisibleLoading(false);
      });
    }
  }, [unitFilter, ps, p, reRender, pageNumber]);


  const handleDelete = (id) => {
    setIdToDelete(id);
    setPopupMessage("Bạn có muốn xoá category này?");
    setPopupVisible(true);
  };

  const handleConfirmDelete = async () => {
    const response = await deletCategory(IdToDelete);
    if (response) {
      enqueueSnackbar("Đã xoá thành công", {
        variant: "success",
      });
      setRender((prev) => !prev);
    } else {
      enqueueSnackbar("Xoá thất bại", {
        variant: "error",
      });
    }
    setPopupVisible(false);
  };

  const handleCancelDelete = () => {
    setPopupVisible(false);
  };




  return (
    <LayoutCommon>
      <div className="title py-3 text-danger">
        <h3>Quản lý Danh mục sản phẩm</h3>
      </div>

      <HeaderBtn>
        <BtnSuccess icon={faAdd} slug={"/admin/category/edit"} name="Thêm mới" />
        <UnitFilter />
      </HeaderBtn>

      <div className="layout_ad_content">
        {isVisibleLoading ? (
          <Loading />
        ) : (
          <Table responsive bordered>
            <thead>
              <tr>
                <th>Tên Category</th>
                <th>Sửa</th>
                <th>Xoá</th>
              </tr>
            </thead>
            <tbody>
              {getCategory.length > 0 ? (
                getCategory.map((item, index) => (
                  <tr key={index}>
                    <td>{item.name}</td>
                    <td className="text-center">
                      <Link
                        to={`/admin/category/edit/${item.id}`}
                        className="text-warning"
                      >
                        <FontAwesomeIcon icon={faEdit} />
                      </Link>
                    </td>
                    <td className="text-center">
                      <div onClick={() => handleDelete(item.id)}>
                        <FontAwesomeIcon icon={faTrash} color="red" />
                      </div>
                    </td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan={3}>
                    <h4 className="text-danger text-center">Không tìm thấy</h4>
                  </td>
                </tr>
              )}
            </tbody>
          </Table>
        )}
        <BtnNextPage metadata={metadata} onPageChange={updatePageNumber} />
      </div>
      {popupVisible && (
        <Popup
          message={popupMessage}
          onCancel={handleCancelDelete}
          onConfirm={handleConfirmDelete}
        />
      )}
    </LayoutCommon>
  );
};
export default AdCategory;
