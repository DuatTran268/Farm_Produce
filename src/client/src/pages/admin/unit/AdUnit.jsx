import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { useSnackbar } from "notistack";
import { useSelector } from "react-redux";
import { Link, useParams } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAdd, faEdit, faTrash } from "@fortawesome/free-solid-svg-icons";
import { Table } from "react-bootstrap";
import Loading from "../../../components/common/Loading";
import BtnNextPage from "../../../components/common/BtnNextPage";
import UnitFilter from "../../../components/admin/filter/UnitFilter";
import { deletUnit, getFilterUnit } from "../../../api/Unit";
import BtnSuccess from "../../../components/common/BtnSuccess";
import HeaderBtn from "../../../components/common/HeaderBtn";

import Popup from "../../../components/common/Popup";

const AdUnit = () => {
  const [getUnit, setgetUnit] = useState([]);
  const [refreshData, setRefreshData] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [metadata, setMetadata] = useState({});
  const [pageNumber, setPageNumber] = useState(1);

  const [isVisibleLoading, setIsVisibleLoading] = useState(true),
    unitFilter = useSelector((state) => state.unitFilter);

  const [popupMessage, setPopupMessage] = useState("");
  const [popupVisible, setPopupVisible] = useState(false);
  const [unitIdToDelete, setUnitIdToDelete] = useState(null);

  let { id } = useParams,
    p = 1,
    ps = 5;
  function updatePageNumber(inc) {
    setPageNumber((currentVal) => currentVal + inc);
  }

  useEffect(() => {
    document.title = "Quản lý Đơn vị tính";

    loadDepartment();
    async function loadDepartment() {
      function setData(props) {
        setgetUnit(props.items);
        setMetadata(props.metadata);
      }
      getFilterUnit(unitFilter.name, ps, pageNumber).then((data) => {
        if (data) {
          setData(data);
        } else {
          setData([]);
        }
        setIsVisibleLoading(false);
      });
    }
  }, [unitFilter, ps, p, refreshData, pageNumber]);

  const handleDeleteUnit = (id) => {
    setUnitIdToDelete(id);
    setPopupMessage("Bạn có muốn xoá unit này?");
    setPopupVisible(true);
  };

  const handleConfirmDelete = async () => {
    const response = await deletUnit(unitIdToDelete);
    if (response) {
      enqueueSnackbar("Đã xoá thành công", {
        variant: "success",
      });
      setRefreshData((prev) => !prev);
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
        <h3>Quản lý Đơn vị tính</h3>
      </div>

      <HeaderBtn>
        <BtnSuccess icon={faAdd} slug={"/admin/unit/edit"} name="Thêm mới" />
        <UnitFilter />
      </HeaderBtn>

      <div className="layout_ad_content">
        {isVisibleLoading ? (
          <Loading />
        ) : (
          <Table responsive bordered>
            <thead>
              <tr>
                <th>Tên Unit</th>
                <th>Sửa</th>
                <th>Xoá</th>
              </tr>
            </thead>
            <tbody>
              {getUnit.length > 0 ? (
                getUnit.map((item, index) => (
                  <tr key={index}>
                    <td>{item.name}</td>
                    <td className="text-center">
                      <Link
                        to={`/admin/unit/edit/${item.id}`}
                        className="text-warning"
                      >
                        <FontAwesomeIcon icon={faEdit} />
                      </Link>
                    </td>
                    <td className="text-center">
                      <div onClick={() => handleDeleteUnit(item.id)}>
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
export default AdUnit;
