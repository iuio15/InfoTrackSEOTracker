import React, { useState } from "react";
import PropTypes from "prop-types";
import '../styles/SearchForm.css';

const SearchForm = ({ onSearch }) => {
    const [keyword, setKeyword] = useState("");
    const [url, setUrl] = useState("");
    const [searchEngine, setSearchEngine] = useState("Google");
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    const handleSubmit = async (e) => {
        e.preventDefault();
        setIsLoading(true);
        setError(null);

        try {
            const response = await fetch("http://localhost:5000/api/search/perform", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ keyword, url, searchEngine }),
            });

            const data = await response.json();

            onSearch(data);
        } catch (err) {
            setError(err.message);
            onSearch(null);
        } finally {
            setIsLoading(false);
        }
    };

    return (
        <form onSubmit={handleSubmit} className="search-form">
            <div className="form-group">
                <input
                    type="text"
                    value={keyword}
                    onChange={(e) => setKeyword(e.target.value)}
                    placeholder="Keyword"
                    required
                />
            </div>
            <div className="form-group">
                <input
                    type="text"
                    value={url}
                    onChange={(e) => setUrl(e.target.value)}
                    placeholder="URL"
                    required
                />
            </div>
            <div className="form-group">
                <select value={searchEngine} onChange={(e) => setSearchEngine(e.target.value)}>
                    <option value="Google">Google</option>
                    <option value="Bing">Bing</option>
                </select>
            </div>
            <button type="submit" className="submit-button" disabled={isLoading}>
                {isLoading ? "Searching..." : "Search"}
            </button>
            {error && <p className="error-message">{error}</p>}
        </form>
    );
};

SearchForm.propTypes = {
    onSearch: PropTypes.func.isRequired,
};

export default SearchForm;